using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

namespace Dota.Port.Editor
{
    public static class HeroDemoExport
    {
        private const string OriginalScene = "Assets/Scenes/Map_hero_demo_main.unity";
        private const string BuildScene = "Assets/IOSPort/HeroDemoBootV14.unity";
        private const string HeroResourcesFolder = "Assets/Resources/HeroDemos";
        private const string HeroMaterialsFolder = "Assets/IOSPort/ShowcaseMaterials";
        private const string FloorMaterialPath = "Assets/IOSPort/ShowcaseFloor.mat";
        private const string BootstrapType = "Dota.App.GameBootstrap";

        public static void PrepareScene()
        {
            var sourceScene = EditorSceneManager.OpenScene(OriginalScene, OpenSceneMode.Single);
            MonoBehaviour bootstrap = null;
            foreach (GameObject root in sourceScene.GetRootGameObjects())
            {
                foreach (MonoBehaviour component in root.GetComponentsInChildren<MonoBehaviour>(true))
                {
                    if (!component || component.GetType().FullName != BootstrapType)
                        continue;
                    if (bootstrap)
                        throw new BuildFailedException("More than one recovered GameBootstrap was found.");
                    bootstrap = component;
                }
            }

            if (!bootstrap)
                throw new BuildFailedException("The recovered hero demo bootstrap script could not be resolved.");

            var original = new SerializedObject(bootstrap);
            var sourcePrefab = original.FindProperty("heroPrefab")?.objectReferenceValue as GameObject;
            var sourceSpawn = original.FindProperty("spawnPoint")?.objectReferenceValue as Transform;
            Quaternion spawnRotation = sourceSpawn ? sourceSpawn.rotation : Quaternion.Euler(0f, 180f, 0f);
            if (!sourcePrefab)
                throw new BuildFailedException("The recovered hero prefab reference is missing.");

            List<OfflineHeroEntry> roster = BuildOfflineRoster(out int defaultHeroIndex);
            if (roster.Count == 0 || string.IsNullOrWhiteSpace(roster[defaultHeroIndex].prefabResourceKey))
                throw new BuildFailedException("The exported hero roster does not contain a usable Shadow Fiend model.");

            var buildScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            if (!buildScene.IsValid())
                throw new BuildFailedException("Could not create the clean v1.4 showcase scene.");

            CreateShowcaseStage();

            var spawnObject = new GameObject("Shadow Fiend Spawn");
            var spawn = spawnObject.transform;
            spawn.position = Vector3.zero;
            spawn.rotation = spawnRotation;

            var cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";
            cameraObject.transform.position = new Vector3(0f, 3f, -8f);
            cameraObject.transform.rotation = Quaternion.Euler(13f, -14f, 0f);
            var camera = cameraObject.AddComponent<Camera>();
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.009f, 0.006f, 0.012f, 1f);
            camera.nearClipPlane = 0.1f;
            camera.farClipPlane = 250f;
            camera.fieldOfView = 36f;

            var replacement = new GameObject("v1.4 Offline Dota App").AddComponent<OfflineDotaApp>();
            var serialized = new SerializedObject(replacement);
            serialized.FindProperty("heroRoster").arraySize = roster.Count;
            SerializedProperty rosterProperty = serialized.FindProperty("heroRoster");
            for (int i = 0; i < roster.Count; i++)
            {
                SerializedProperty item = rosterProperty.GetArrayElementAtIndex(i);
                item.FindPropertyRelative("key").stringValue = roster[i].key;
                item.FindPropertyRelative("displayName").stringValue = roster[i].displayName;
                item.FindPropertyRelative("prefabResourceKey").stringValue = roster[i].prefabResourceKey;
                item.FindPropertyRelative("portrait").objectReferenceValue = roster[i].portrait;
                SerializedProperty icons = item.FindPropertyRelative("abilityIcons");
                SerializedProperty names = item.FindPropertyRelative("abilityNames");
                icons.arraySize = 4;
                names.arraySize = 4;
                for (int slot = 0; slot < 4; slot++)
                {
                    icons.GetArrayElementAtIndex(slot).objectReferenceValue = roster[i].abilityIcons[slot];
                    names.GetArrayElementAtIndex(slot).stringValue = roster[i].abilityNames[slot];
                }
            }
            serialized.FindProperty("defaultHeroIndex").intValue = defaultHeroIndex;
            serialized.FindProperty("menuBackground").objectReferenceValue = LoadSprite("menu", "dashboard_background");
            serialized.FindProperty("dotaLogo").objectReferenceValue = LoadSprite("menu", "dota_logo");
            serialized.FindProperty("playButtonArt").objectReferenceValue = LoadSprite("menu", "background_play_button_2x");
            serialized.FindProperty("spawnPoint").objectReferenceValue = spawn;
            serialized.FindProperty("demoCamera").objectReferenceValue = camera;
            serialized.ApplyModifiedPropertiesWithoutUndo();

            if (!EditorSceneManager.SaveScene(buildScene, BuildScene, true))
                throw new BuildFailedException("Could not save the iOS demo boot scene.");
            AssetDatabase.SaveAssets();
            Debug.Log($"[V14Offline] Prepared {BuildScene}: {roster.Count} hero portraits, {CountPlayableHeroes(roster)} local 3D demos, offline menu and HUD.");
        }

        private static List<OfflineHeroEntry> BuildOfflineRoster(out int defaultHeroIndex)
        {
            const string heroDataPath = "Assets/IOSPort/SourceAssetArchive/gamedata/heroes.json";
            string absoluteHeroDataPath = Path.Combine(Application.dataPath, "IOSPort/SourceAssetArchive/gamedata/heroes.json");
            if (!File.Exists(absoluteHeroDataPath))
                throw new BuildFailedException($"Recovered hero data is missing: {heroDataPath}");

            Debug.Log($"[V14Offline] Reading recovered hero data: {absoluteHeroDataPath}");
            string heroJson = File.ReadAllText(absoluteHeroDataPath);
            List<KeyValuePair<string, string>> source = ParseJsonObjectProperties(heroJson);
            Debug.Log($"[V14Offline] Parsed {source.Count} hero-data records.");
            var roster = new List<OfflineHeroEntry>();
            defaultHeroIndex = -1;
            foreach (KeyValuePair<string, string> property in source)
            {
                const string prefix = "npc_dota_hero_";
                if (!property.Key.StartsWith(prefix, StringComparison.Ordinal))
                    continue;

                string slug = property.Key.Substring(prefix.Length);
                if (slug == "base" || slug == "target_dummy")
                    continue;
                Dictionary<string, string> hero = ParseJsonObject(property.Value);

                var entry = new OfflineHeroEntry
                {
                    key = slug,
                    displayName = GetHeroDisplayName(slug),
                    prefabResourceKey = string.Empty,
                    portrait = LoadSprite("heroicons", property.Key),
                    abilityIcons = new Sprite[4],
                    abilityNames = new string[4]
                };
                if (!entry.portrait)
                    entry.portrait = LoadSprite("heroportraits", property.Key);

                List<string> abilities = GetShowcaseAbilities(hero);
                for (int slot = 0; slot < 4; slot++)
                {
                    string ability = slot < abilities.Count ? abilities[slot] : string.Empty;
                    entry.abilityNames[slot] = string.IsNullOrEmpty(ability) ? $"Ability {slot + 1}" : GetAbilityDisplayName(slug, ability);
                    if (!string.IsNullOrEmpty(ability))
                        entry.abilityIcons[slot] = LoadSprite("spellicons", ability) ?? LoadSprite("hudicons", ability);
                }

                string sourcePrefabPath = FindHeroPrefabPath(slug);
                if (!string.IsNullOrEmpty(sourcePrefabPath))
                {
                    var sourcePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(sourcePrefabPath);
                    if (sourcePrefab)
                    {
                        string destinationPath = $"{HeroResourcesFolder}/{slug}.prefab";
                        try
                        {
                            CreateShowcasePrefab(sourcePrefab, slug, destinationPath);
                            entry.prefabResourceKey = $"HeroDemos/{slug}";
                        }
                        catch (Exception exception)
                        {
                            Debug.LogWarning($"[V14Offline] {entry.displayName} portrait is available, but its 3D demo could not be prepared: {exception.Message}");
                        }
                    }
                }

                if (!entry.portrait)
                    Debug.LogWarning($"[V14Offline] No portrait sprite was found for {property.Key}.");
                if (slug == "nevermore")
                    defaultHeroIndex = roster.Count;
                roster.Add(entry);
            }

            if (defaultHeroIndex < 0)
                throw new BuildFailedException("Shadow Fiend is missing from the recovered hero roster.");
            if (string.IsNullOrEmpty(roster[defaultHeroIndex].prefabResourceKey))
                throw new BuildFailedException("The recovered Shadow Fiend model could not be prepared as an offline demo.");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return roster;
        }

        private static int CountPlayableHeroes(List<OfflineHeroEntry> roster)
        {
            int count = 0;
            foreach (OfflineHeroEntry hero in roster)
                if (!string.IsNullOrEmpty(hero.prefabResourceKey))
                    count++;
            return count;
        }

        private static string FindHeroPrefabPath(string slug)
        {
            string[] candidates = { slug, GetModelAlias(slug) };
            foreach (string candidate in candidates)
            {
                if (string.IsNullOrEmpty(candidate))
                    continue;
                string path = $"Assets/GameObject/{candidate}.prefab";
                if (AssetDatabase.LoadAssetAtPath<GameObject>(path))
                    return path;
            }
            return null;
        }

        private static string GetModelAlias(string slug)
        {
            switch (slug)
            {
                case "bloodseeker": return "blood_seeker";
                case "drow_ranger": return "drow";
                case "nevermore": return "shadow_fiend";
                case "queenofpain": return "queen_of_pain";
                case "skeleton_king": return "wraith_king";
                case "windrunner": return "windranger";
                case "zuus": return "zeus";
                default: return slug;
            }
        }

        private static string GetHeroDisplayName(string slug)
        {
            switch (slug)
            {
                case "antimage": return "Anti-Mage";
                case "bloodseeker": return "Bloodseeker";
                case "crystal_maiden": return "Crystal Maiden";
                case "earthshaker": return "Earthshaker";
                case "drow_ranger": return "Drow Ranger";
                case "life_stealer": return "Lifestealer";
                case "nevermore": return "Shadow Fiend";
                case "queenofpain": return "Queen of Pain";
                case "shadow_demon": return "Shadow Demon";
                case "skeleton_king": return "Wraith King";
                case "storm_spirit": return "Storm Spirit";
                case "windrunner": return "Windranger";
                case "zuus": return "Zeus";
                case "wisp": return "Io";
                case "furion": return "Nature's Prophet";
                case "shredder": return "Timbersaw";
                case "rattletrap": return "Clockwerk";
                case "obsidian_destroyer": return "Outworld Destroyer";
                case "treant": return "Treant Protector";
            }
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(slug.Replace('_', ' '));
        }

        private static List<string> GetShowcaseAbilities(Dictionary<string, string> hero)
        {
            var regular = new List<string>();
            string ultimate = null;
            var definitions = new Dictionary<string, string>(StringComparer.Ordinal);
            if (hero.TryGetValue("AbilityDefinitions", out string definitionsJson))
            {
                foreach (KeyValuePair<string, string> definition in ParseJsonObjectProperties(definitionsJson))
                    definitions[definition.Key] = definition.Value;
            }

            for (int index = 1; index <= 6; index++)
            {
                if (!hero.TryGetValue("Ability" + index, out string abilityJson))
                    continue;
                string ability = ParseJsonStringValue(abilityJson);
                if (string.IsNullOrWhiteSpace(ability) || ability.StartsWith("generic_hidden", StringComparison.Ordinal))
                    continue;

                string type = null;
                if (definitions.TryGetValue(ability, out string definitionJson))
                {
                    Dictionary<string, string> definition = ParseJsonObject(definitionJson);
                    if (definition.TryGetValue("AbilityType", out string typeJson))
                        type = ParseJsonStringValue(typeJson);
                }
                if (!string.IsNullOrEmpty(type) && type.IndexOf("ULTIMATE", StringComparison.OrdinalIgnoreCase) >= 0)
                    ultimate = ability;
                else if (!regular.Contains(ability))
                    regular.Add(ability);
            }

            var result = new List<string>();
            for (int i = 0; i < regular.Count && result.Count < 3; i++)
                result.Add(regular[i]);
            if (!string.IsNullOrEmpty(ultimate))
                result.Add(ultimate);
            for (int i = 0; i < regular.Count && result.Count < 4; i++)
                if (!result.Contains(regular[i]))
                    result.Add(regular[i]);
            return result;
        }

        private static Dictionary<string, string> ParseJsonObject(string json)
        {
            var properties = ParseJsonObjectProperties(json);
            var result = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (KeyValuePair<string, string> property in properties)
                result[property.Key] = property.Value;
            return result;
        }

        private static List<KeyValuePair<string, string>> ParseJsonObjectProperties(string json)
        {
            var properties = new List<KeyValuePair<string, string>>();
            int index = 0;
            SkipJsonWhitespace(json, ref index);
            if (index >= json.Length || json[index] != '{')
                throw new BuildFailedException("Recovered hero data contains a value that is not a JSON object.");
            index++;

            while (index < json.Length)
            {
                SkipJsonWhitespace(json, ref index);
                if (json[index] == '}')
                    return properties;
                string name = ReadJsonString(json, ref index);
                SkipJsonWhitespace(json, ref index);
                if (index >= json.Length || json[index++] != ':')
                    throw new BuildFailedException("Recovered hero data has a malformed JSON property.");
                SkipJsonWhitespace(json, ref index);
                int valueStart = index;
                SkipJsonValue(json, ref index);
                properties.Add(new KeyValuePair<string, string>(name, json.Substring(valueStart, index - valueStart)));
                SkipJsonWhitespace(json, ref index);
                if (index < json.Length && json[index] == ',')
                {
                    index++;
                    continue;
                }
                if (index < json.Length && json[index] == '}')
                    return properties;
                throw new BuildFailedException("Recovered hero data has a malformed JSON object boundary.");
            }

            throw new BuildFailedException("Recovered hero data ended before its JSON object was closed.");
        }

        private static string ParseJsonStringValue(string json)
        {
            int index = 0;
            SkipJsonWhitespace(json, ref index);
            if (index >= json.Length || json[index] != '"')
                return null;
            string value = ReadJsonString(json, ref index);
            SkipJsonWhitespace(json, ref index);
            return index == json.Length ? value : null;
        }

        private static void SkipJsonWhitespace(string json, ref int index)
        {
            while (index < json.Length && char.IsWhiteSpace(json[index]))
                index++;
        }

        private static string ReadJsonString(string json, ref int index)
        {
            if (index >= json.Length || json[index++] != '"')
                throw new BuildFailedException("Recovered hero data has a malformed JSON string.");
            var value = new System.Text.StringBuilder();
            while (index < json.Length)
            {
                char current = json[index++];
                if (current == '"')
                    return value.ToString();
                if (current != '\\')
                {
                    value.Append(current);
                    continue;
                }
                if (index >= json.Length)
                    break;
                char escape = json[index++];
                switch (escape)
                {
                    case '"': value.Append('"'); break;
                    case '\\': value.Append('\\'); break;
                    case '/': value.Append('/'); break;
                    case 'b': value.Append('\b'); break;
                    case 'f': value.Append('\f'); break;
                    case 'n': value.Append('\n'); break;
                    case 'r': value.Append('\r'); break;
                    case 't': value.Append('\t'); break;
                    case 'u':
                        if (index + 4 > json.Length || !ushort.TryParse(json.Substring(index, 4), NumberStyles.HexNumber,
                            CultureInfo.InvariantCulture, out ushort codePoint))
                            throw new BuildFailedException("Recovered hero data contains an invalid JSON unicode escape.");
                        value.Append((char)codePoint);
                        index += 4;
                        break;
                    default: throw new BuildFailedException("Recovered hero data contains an invalid JSON escape.");
                }
            }
            throw new BuildFailedException("Recovered hero data contains an unterminated JSON string.");
        }

        private static void SkipJsonValue(string json, ref int index)
        {
            if (index >= json.Length)
                throw new BuildFailedException("Recovered hero data ended before a JSON value.");
            if (json[index] == '"')
            {
                ReadJsonString(json, ref index);
                return;
            }
            if (json[index] == '{' || json[index] == '[')
            {
                var closing = new Stack<char>();
                closing.Push(json[index] == '{' ? '}' : ']');
                index++;
                while (index < json.Length && closing.Count > 0)
                {
                    char current = json[index];
                    if (current == '"')
                    {
                        ReadJsonString(json, ref index);
                        continue;
                    }
                    if (current == '{' || current == '[')
                        closing.Push(current == '{' ? '}' : ']');
                    else if (current == '}' || current == ']')
                    {
                        if (closing.Pop() != current)
                            throw new BuildFailedException("Recovered hero data has mismatched JSON brackets.");
                    }
                    index++;
                }
                if (closing.Count != 0)
                    throw new BuildFailedException("Recovered hero data ended before a JSON value was closed.");
                return;
            }

            while (index < json.Length && json[index] != ',' && json[index] != '}' && json[index] != ']' && !char.IsWhiteSpace(json[index]))
                index++;
        }

        private static string GetAbilityDisplayName(string heroSlug, string ability)
        {
            string name = ability;
            string prefix = heroSlug + "_";
            if (name.StartsWith(prefix, StringComparison.Ordinal))
                name = name.Substring(prefix.Length);
            else if (heroSlug == "nevermore" && name.StartsWith("nevermore_", StringComparison.Ordinal))
                name = name.Substring("nevermore_".Length);
            name = name.Replace('_', ' ');
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(name);
        }

        private static Sprite LoadSprite(string folder, string fileName)
        {
            string path = $"Assets/IOSPort/SourceAssetArchive/{folder}/{fileName}.asset";
            return AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }

        private static void EnsureFolder(string folder)
        {
            if (string.IsNullOrEmpty(folder) || AssetDatabase.IsValidFolder(folder))
                return;
            string parent = Path.GetDirectoryName(folder)?.Replace('\\', '/');
            string leaf = Path.GetFileName(folder);
            EnsureFolder(parent);
            if (!string.IsNullOrEmpty(parent) && !AssetDatabase.IsValidFolder(folder))
                AssetDatabase.CreateFolder(parent, leaf);
        }

        private static GameObject CreateShowcasePrefab(GameObject sourcePrefab, string heroSlug, string destinationPath)
        {
            string sourcePath = AssetDatabase.GetAssetPath(sourcePrefab);
            if (string.IsNullOrWhiteSpace(sourcePath) || !PrefabUtility.IsPartOfPrefabAsset(sourcePrefab))
                throw new BuildFailedException($"The recovered {heroSlug} hero reference is not a prefab asset.");

            var contents = PrefabUtility.LoadPrefabContents(sourcePath);
            try
            {
                int removedScripts = 0;
                foreach (var behaviour in contents.GetComponentsInChildren<MonoBehaviour>(true))
                {
                    if (!behaviour)
                        continue;

                    string assembly = behaviour.GetType().Assembly.GetName().Name;
                    if (!assembly.StartsWith("Dota.", StringComparison.Ordinal) && assembly != "LiteNetLib")
                        continue;

                    UnityEngine.Object.DestroyImmediate(behaviour, true);
                    removedScripts++;
                }

                Shader showcaseShader = Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard");
                if (!showcaseShader)
                    throw new BuildFailedException("Could not find a shader for offline hero showcase materials.");

                EnsureFolder(HeroMaterialsFolder);
                EnsureFolder(Path.GetDirectoryName(destinationPath)?.Replace('\\', '/'));

                var convertedMaterials = new HashSet<string>(StringComparer.Ordinal);
                foreach (Renderer renderer in contents.GetComponentsInChildren<Renderer>(true))
                {
                    var materials = renderer.sharedMaterials;
                    for (int i = 0; i < materials.Length; i++)
                    {
                        Material sourceMaterial = materials[i];
                        if (!sourceMaterial || !sourceMaterial.shader || sourceMaterial.shader.name != "Dota/Hero")
                            continue;

                        materials[i] = CreateShowcaseMaterial(sourceMaterial, showcaseShader, heroSlug);
                        convertedMaterials.Add(sourceMaterial.name);
                    }

                    renderer.sharedMaterials = materials;
                    renderer.shadowCastingMode = ShadowCastingMode.Off;
                    renderer.receiveShadows = false;
                }

                var cleanPrefab = PrefabUtility.SaveAsPrefabAsset(contents, destinationPath);
                if (!cleanPrefab)
                    throw new BuildFailedException($"Could not save the script-free {heroSlug} showcase prefab.");

                Debug.Log($"[V14Offline] Created {destinationPath} from {sourcePath}; converted {convertedMaterials.Count} hero materials and removed {removedScripts} Dota/network components.");
                return cleanPrefab;
            }
            finally
            {
                PrefabUtility.UnloadPrefabContents(contents);
            }
        }

        private static Material CreateShowcaseMaterial(Material source, Shader shader, string heroSlug)
        {
            Texture colorTexture = source.GetTexture("_ColorTex");
            if (!colorTexture)
                throw new BuildFailedException($"{source.name} has no recovered _ColorTex texture.");

            string path = $"{HeroMaterialsFolder}/{heroSlug}_{source.name}_Showcase.mat";
            var material = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (!material)
            {
                material = new Material(shader) { name = $"{source.name}_Showcase" };
                AssetDatabase.CreateAsset(material, path);
            }
            else
            {
                material.shader = shader;
            }

            string colorProperty = material.HasProperty("_BaseMap") ? "_BaseMap" : "_MainTex";
            if (material.HasProperty(colorProperty))
            {
                material.SetTexture(colorProperty, colorTexture);
                material.SetTextureScale(colorProperty, source.GetTextureScale("_ColorTex"));
                material.SetTextureOffset(colorProperty, source.GetTextureOffset("_ColorTex"));
            }

            Color tint = source.HasProperty("_ObjectTint") ? source.GetColor("_ObjectTint") : Color.white;
            if (material.HasProperty("_BaseColor"))
                material.SetColor("_BaseColor", tint);
            if (material.HasProperty("_Color"))
                material.SetColor("_Color", tint);

            Texture normalTexture = source.GetTexture("_NormalTex");
            if (normalTexture && material.HasProperty("_BumpMap"))
            {
                material.SetTexture("_BumpMap", normalTexture);
                material.SetTextureScale("_BumpMap", source.GetTextureScale("_NormalTex"));
                material.SetTextureOffset("_BumpMap", source.GetTextureOffset("_NormalTex"));
                material.EnableKeyword("_NORMALMAP");
            }

            if (material.HasProperty("_Metallic"))
                material.SetFloat("_Metallic", 0f);
            if (material.HasProperty("_Smoothness"))
                material.SetFloat("_Smoothness", 0.22f);
            if (material.HasProperty("_Surface"))
                material.SetFloat("_Surface", 0f);
            material.renderQueue = (int)RenderQueue.Geometry;
            EditorUtility.SetDirty(material);
            return material;
        }

        private static void CreateShowcaseStage()
        {
            RenderSettings.ambientMode = AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(0.26f, 0.20f, 0.24f, 1f);
            RenderSettings.skybox = null;

            var floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
            floor.name = "Dota Showcase Floor";
            floor.transform.position = new Vector3(0f, -0.12f, 0f);
            floor.transform.localScale = new Vector3(10f, 1f, 10f);
            floor.GetComponent<Renderer>().sharedMaterial = GetOrCreateMaterial(
                FloorMaterialPath, new Color(0.018f, 0.012f, 0.022f, 1f));

            var lightObject = new GameObject("Showcase Key Light");
            lightObject.transform.rotation = Quaternion.Euler(48f, -28f, 0f);
            var light = lightObject.AddComponent<Light>();
            light.type = LightType.Directional;
            light.color = new Color(1f, 0.78f, 0.73f, 1f);
            light.intensity = 1.65f;
            light.shadows = LightShadows.None;
        }

        private static Material GetOrCreateMaterial(string path, Color color)
        {
            var material = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (!material)
            {
                var shader = Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard");
                if (!shader)
                    throw new BuildFailedException("Could not find a shader for the showcase stage.");

                material = new Material(shader);
                AssetDatabase.CreateAsset(material, path);
            }

            material.color = color;
            EditorUtility.SetDirty(material);
            return material;
        }

        public static void ExportIOS()
        {
            PrepareScene();
            string output = Environment.GetEnvironmentVariable("DOTA_V14_XCODE_OUTPUT");
            if (string.IsNullOrWhiteSpace(output))
                throw new BuildFailedException("Set DOTA_V14_XCODE_OUTPUT to a new export directory.");
            if (Directory.Exists(output) && Directory.GetFileSystemEntries(output).Length > 0)
                throw new BuildFailedException("Choose an empty export directory.");

            PlayerSettings.SetApplicationIdentifier(NamedBuildTarget.iOS, "com.temka.dota2mobile");
            PlayerSettings.SetScriptingBackend(NamedBuildTarget.iOS, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetUseDefaultGraphicsAPIs(BuildTarget.iOS, false);
            PlayerSettings.SetGraphicsAPIs(BuildTarget.iOS, new[] { GraphicsDeviceType.Metal });
            PlayerSettings.iOS.targetOSVersionString = "15.0";
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;


            var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = new[] { BuildScene },
                target = BuildTarget.iOS,
                locationPathName = output
            });
            if (report.summary.result != BuildResult.Succeeded)
                throw new BuildFailedException($"iOS export failed: {report.summary.result}");
            Debug.Log($"[V14Demo] Exported iOS Xcode project to {output}");
        }
    }
}

