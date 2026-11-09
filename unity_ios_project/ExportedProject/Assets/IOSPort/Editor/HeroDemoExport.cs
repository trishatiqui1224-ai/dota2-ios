using System;
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
        private const string DemoPrefabPath = "Assets/IOSPort/ShadowFiendDemo.prefab";
        private const string FloorMaterialPath = "Assets/IOSPort/ShowcaseFloor.mat";
        private const string PlatformMaterialPath = "Assets/IOSPort/ShowcasePlatform.mat";
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

            var prefab = CreateShowcasePrefab(sourcePrefab);
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
            camera.backgroundColor = new Color(0.025f, 0.035f, 0.05f, 1f);
            camera.nearClipPlane = 0.1f;
            camera.farClipPlane = 250f;
            camera.fieldOfView = 36f;

            var replacement = new GameObject("v1.4 Hero Demo Boot").AddComponent<HeroDemoBoot>();
            var serialized = new SerializedObject(replacement);
            serialized.FindProperty("heroPrefab").objectReferenceValue = prefab;
            serialized.FindProperty("spawnPoint").objectReferenceValue = spawn;
            serialized.FindProperty("demoCamera").objectReferenceValue = camera;
            serialized.ApplyModifiedPropertiesWithoutUndo();

            if (!EditorSceneManager.SaveScene(buildScene, BuildScene, true))
                throw new BuildFailedException("Could not save the iOS demo boot scene.");
            AssetDatabase.SaveAssets();
            Debug.Log($"[V14Demo] Prepared {BuildScene} with sanitized prefab {prefab.name}, a clean stage, and a fitted camera.");
        }

        private static GameObject CreateShowcasePrefab(GameObject sourcePrefab)
        {
            string sourcePath = AssetDatabase.GetAssetPath(sourcePrefab);
            if (string.IsNullOrWhiteSpace(sourcePath) || !PrefabUtility.IsPartOfPrefabAsset(sourcePrefab))
                throw new BuildFailedException("The recovered hero reference is not a prefab asset.");

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

                var cleanPrefab = PrefabUtility.SaveAsPrefabAsset(contents, DemoPrefabPath);
                if (!cleanPrefab)
                    throw new BuildFailedException("Could not save the script-free hero showcase prefab.");

                Debug.Log($"[V14Demo] Created {DemoPrefabPath} from {sourcePath}; removed {removedScripts} Dota/network components.");
                return cleanPrefab;
            }
            finally
            {
                PrefabUtility.UnloadPrefabContents(contents);
            }
        }

        private static void CreateShowcaseStage()
        {
            RenderSettings.ambientMode = AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(0.16f, 0.19f, 0.25f, 1f);
            RenderSettings.skybox = null;

            var floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
            floor.name = "Dota Showcase Floor";
            floor.transform.position = new Vector3(0f, -0.12f, 0f);
            floor.transform.localScale = new Vector3(10f, 1f, 10f);
            floor.GetComponent<Renderer>().sharedMaterial = GetOrCreateMaterial(
                FloorMaterialPath, new Color(0.055f, 0.07f, 0.09f, 1f));

            var platform = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            platform.name = "Hero Display Platform";
            platform.transform.position = new Vector3(0f, -0.08f, 0f);
            platform.transform.localScale = new Vector3(3.2f, 0.08f, 3.2f);
            platform.GetComponent<Renderer>().sharedMaterial = GetOrCreateMaterial(
                PlatformMaterialPath, new Color(0.14f, 0.18f, 0.24f, 1f));

            var lightObject = new GameObject("Showcase Key Light");
            lightObject.transform.rotation = Quaternion.Euler(48f, -28f, 0f);
            var light = lightObject.AddComponent<Light>();
            light.type = LightType.Directional;
            light.color = new Color(0.74f, 0.83f, 1f, 1f);
            light.intensity = 1.35f;
            light.shadows = LightShadows.Soft;
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

