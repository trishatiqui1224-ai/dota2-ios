using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dota.App
{
    public class DotaSceneDiagnostic : MonoBehaviour
    {
        private static DotaSceneDiagnostic instance;
        private static bool hasDumped = false;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AutoBoot()
        {
            if (instance != null) return;
            GameObject go = new GameObject("[DotaSceneDiagnostic]");
            DontDestroyOnLoad(go);
            instance = go.AddComponent<DotaSceneDiagnostic>();
        }

        private void Start()
        {
            StartCoroutine(DiagnosticTimerRoutine());
        }

        private IEnumerator DiagnosticTimerRoutine()
        {
            yield return new WaitForSeconds(4.0f);
            ExecuteDump();
        }

        public static void ExecuteDump()
        {
            if (hasDumped) return;
            hasDumped = true;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("==================================================");
            sb.AppendLine("  DOTA 2 MOBILE iOS — SCENE & BUILD HIERARCHY DUMP (4.0s)");
            sb.AppendLine("==================================================");

            // Step 1.1: Build Settings Scenes
            int buildSceneCount = SceneManager.sceneCountInBuildSettings;
            sb.AppendLine($"[BuildSettings] Total Scenes: {buildSceneCount}");
            for (int i = 0; i < buildSceneCount; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                sb.AppendLine($"  Scene [{i}]: {scenePath}");
            }

            // Step 1.2: Active Scene Details
            Scene activeScene = SceneManager.GetActiveScene();
            sb.AppendLine($"\n[ActiveScene] Name: '{activeScene.name}' | BuildIndex: {activeScene.buildIndex} | IsLoaded: {activeScene.isLoaded} | RootCount: {activeScene.rootCount}");

            // Step 1.3: Active Scene GameObjects
            sb.AppendLine("\n--- Active Scene GameObjects Hierarchy ---");
            GameObject[] activeRoots = activeScene.GetRootGameObjects();
            sb.AppendLine($"Total Root GameObjects in Active Scene: {activeRoots.Length}");
            foreach (GameObject root in activeRoots)
            {
                DumpGameObjectRecursive(root, 0, sb);
            }

            // Step 1.3: DontDestroyOnLoad GameObjects
            sb.AppendLine("\n--- DontDestroyOnLoad GameObjects Hierarchy ---");
            GameObject tempDDOL = new GameObject("[TempDDOLProbe]");
            DontDestroyOnLoad(tempDDOL);
            Scene ddolScene = tempDDOL.scene;
            GameObject[] ddolRoots = ddolScene.GetRootGameObjects();
            sb.AppendLine($"Total Root GameObjects in DontDestroyOnLoad: {ddolRoots.Length - 1}");
            foreach (GameObject root in ddolRoots)
            {
                if (root != tempDDOL)
                {
                    DumpGameObjectRecursive(root, 0, sb);
                }
            }
            Destroy(tempDDOL);

            // Step 3: Audit and Fix any Inactive Canvases / Screen Space - Camera issues
            sb.AppendLine("\n--- Canvas Audit & Runtime Auto-Fix ---");
            Canvas[] allCanvases = Resources.FindObjectsOfTypeAll<Canvas>();
            sb.AppendLine($"Total Canvases in Memory: {allCanvases.Length}");
            foreach (Canvas canvas in allCanvases)
            {
                if (canvas == null) continue;
                sb.AppendLine($"  Canvas '{canvas.name}' (GameObject: {canvas.gameObject.name}, Active: {canvas.gameObject.activeInHierarchy}, RenderMode: {canvas.renderMode})");
                
                // Ensure active
                if (!canvas.gameObject.activeSelf)
                {
                    canvas.gameObject.SetActive(true);
                    sb.AppendLine($"    -> Fixed: SetActive(true) on '{canvas.gameObject.name}'");
                }
                
                // If Screen Space - Camera without camera or needs overlay fix
                if (canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera == null)
                {
                    if (Camera.main != null)
                    {
                        canvas.worldCamera = Camera.main;
                        sb.AppendLine($"    -> Fixed: Assigned Camera.main to canvas.worldCamera");
                    }
                    else
                    {
                        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                        sb.AppendLine($"    -> Fixed: Switched renderMode to ScreenSpaceOverlay");
                    }
                }
                canvas.enabled = true;

                // Fix CanvasGroup alpha
                CanvasGroup[] groups = canvas.GetComponentsInChildren<CanvasGroup>(true);
                foreach (CanvasGroup cg in groups)
                {
                    if (cg != null)
                    {
                        cg.alpha = 1.0f;
                        cg.interactable = true;
                        cg.blocksRaycasts = true;
                        sb.AppendLine($"    -> Fixed CanvasGroup on '{cg.gameObject.name}': alpha=1.0, interactable=true");
                    }
                }
            }

            sb.AppendLine("==================================================");
            sb.AppendLine("  END OF SCENE & BUILD HIERARCHY DUMP");
            sb.AppendLine("==================================================");

            string fullReport = sb.ToString();

            // Log to Unity Debug.Log (forwarded to os_log & dota2_ios_boot.log via Unity/Logging.mm)
            Debug.Log(fullReport);

            // Also directly append to dota2_ios_boot.log in Documents directory
            try
            {
                string logFile = Path.Combine(Application.persistentDataPath, "dota2_ios_boot.log");
                File.AppendAllText(logFile, fullReport + "\n");
            }
            catch (Exception ex)
            {
                Debug.LogWarning("[DotaSceneDiagnostic] Could not append directly to log file: " + ex.Message);
            }
        }

        private static void DumpGameObjectRecursive(GameObject go, int depth, StringBuilder sb)
        {
            if (go == null) return;

            string indent = new string(' ', depth * 2);
            string layerName = LayerMask.LayerToName(go.layer);
            sb.AppendLine($"{indent}* GameObject: '{go.name}' | activeSelf: {go.activeSelf} | activeInHierarchy: {go.activeInHierarchy} | Layer: {go.layer} ({layerName})");

            // Inspect all components on this GameObject (including null for Missing Scripts)
            Component[] components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                Component c = components[i];
                if (c == null)
                {
                    sb.AppendLine($"{indent}  - [CRITICAL MISSING SCRIPT] Component [{i}] on '{go.name}' is NULL (Missing Script Behaviour)!");
                    continue;
                }

                string typeName = c.GetType().Name;
                sb.AppendLine($"{indent}  - Component [{i}]: {typeName}");

                // Canvas details
                if (c is Canvas canvas)
                {
                    string camName = canvas.worldCamera != null ? canvas.worldCamera.name : "null";
                    sb.AppendLine($"{indent}    [Canvas] renderMode: {canvas.renderMode}, enabled: {canvas.enabled}, worldCamera: '{camName}', scaleFactor: {canvas.scaleFactor}");
                }

                // CanvasGroup details
                if (c is CanvasGroup cg)
                {
                    sb.AppendLine($"{indent}    [CanvasGroup] alpha: {cg.alpha:F2}, interactable: {cg.interactable}, blocksRaycasts: {cg.blocksRaycasts}");
                }

                // Renderer details (MeshRenderer, SkinnedMeshRenderer, etc.)
                if (c is Renderer rend)
                {
                    sb.AppendLine($"{indent}    [{typeName}] enabled: {rend.enabled}, isVisible: {rend.isVisible}");
                    Material[] mats = rend.sharedMaterials;
                    if (mats != null && mats.Length > 0)
                    {
                        for (int m = 0; m < mats.Length; m++)
                        {
                            Material mat = mats[m];
                            if (mat == null)
                            {
                                sb.AppendLine($"{indent}      Material [{m}]: NULL");
                            }
                            else
                            {
                                string shaderName = mat.shader != null ? mat.shader.name : "null";
                                bool supported = mat.shader != null && mat.shader.isSupported;
                                sb.AppendLine($"{indent}      Material [{m}]: '{mat.name}' | Shader: '{shaderName}' (supported: {supported})");
                            }
                        }
                    }
                }

                // UI Image / Graphic details
                if (c is Graphic graphic)
                {
                    string matShader = graphic.material != null && graphic.material.shader != null ? graphic.material.shader.name : "default";
                    sb.AppendLine($"{indent}    [Graphic/UI] enabled: {graphic.enabled}, shader: '{matShader}'");
                }

                // Camera details
                if (c is Camera cam)
                {
                    sb.AppendLine($"{indent}    [Camera] enabled: {cam.enabled}, clearFlags: {cam.clearFlags}, bg: {cam.backgroundColor}, depth: {cam.depth}");
                }
            }

            // Recurse children
            Transform t = go.transform;
            int childCount = t.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = t.GetChild(i);
                if (child != null)
                {
                    DumpGameObjectRecursive(child.gameObject, depth + 1, sb);
                }
            }
        }
    }
}
