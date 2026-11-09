#if UNITY_EDITOR && UNITY_IOS
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace Dota.EditorScripts
{
    /// <summary>
    /// Automatically configures the Xcode project during export.
    /// Injects CADisableMinimumFrameDurationOnPhone to unlock 120Hz ProMotion on iPhones,
    /// configures Metal graphics flags, and sets required iOS deployment properties.
    /// </summary>
    public static class IOSBuildPostProcessor
    {
        [PostProcessBuild(999)]
        public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target != BuildTarget.iOS) return;

            string plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
            if (!File.Exists(plistPath)) return;

            PlistDocument plist = new PlistDocument();
            plist.ReadFromFile(plistPath);

            PlistElementDict rootDict = plist.root;

            // 1. CRITICAL FOR 120Hz PROMOTION ON IPHONE:
            // Unlocks 120Hz refresh rate on iPhone 13 Pro, 14 Pro, 15 Pro, 16 Pro models.
            rootDict.SetBoolean("CADisableMinimumFrameDurationOnPhone", true);

            // 2. Prevent App Store export compliance prompts
            rootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false);

            // 3. Ensure UI requires fullscreen on iPad (no split-screen disruption during match)
            rootDict.SetBoolean("UIRequiresFullScreen", true);

            // 4. Hide home bar automatically during gameplay
            rootDict.SetBoolean("UIViewControllerBasedStatusBarAppearance", false);

            // Write updated Info.plist back to disk
            plist.WriteToFile(plistPath);

            // Configure Xcode PBXProject for Metal and arm64 optimizations
            string pbxPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
            if (File.Exists(pbxPath))
            {
                PBXProject project = new PBXProject();
                project.ReadFromFile(pbxPath);

                string mainTargetGuid = project.GetUnityMainTargetGuid();
                string frameworkTargetGuid = project.GetUnityFrameworkTargetGuid();

                // Enable Metal Fast Math and aggressive LLVM compiler optimizations
                project.SetBuildProperty(mainTargetGuid, "ENABLE_BITCODE", "NO");
                project.SetBuildProperty(frameworkTargetGuid, "ENABLE_BITCODE", "NO");

                project.SetBuildProperty(frameworkTargetGuid, "MTL_FAST_MATH", "YES");
                project.SetBuildProperty(frameworkTargetGuid, "GCC_OPTIMIZATION_LEVEL", "3");

                project.WriteToFile(pbxPath);
            }
        }
    }
}
#endif
