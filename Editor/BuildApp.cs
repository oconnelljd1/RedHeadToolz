using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;
using System.Linq;
using RedHeadToolz.Debugging;
using System.Diagnostics;

public static class BuildApp
{
    [MenuItem("RedHeadToolz/Build/Build Application")]
    public static void BuildApplication()
    {
        // Find project root, then go back a step
        string projectPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Application.dataPath, ".."));
        string buildFolder = System.IO.Path.Combine(projectPath, "Builds");
        
        // Create the build folder if it doesn't exist
        if (!System.IO.Directory.Exists(buildFolder))
        {
            System.IO.Directory.CreateDirectory(buildFolder);
        }

        var targetPlatform = EditorUserBuildSettings.activeBuildTarget;
        string platformFolder = System.IO.Path.Combine(buildFolder, targetPlatform.ToString());

        // Create the platform folder if it doesn't exist
        if (!System.IO.Directory.Exists(platformFolder))
        {
            System.IO.Directory.CreateDirectory(platformFolder);
        }

        string projectName = Application.productName;
        string version = PlayerSettings.bundleVersion.Replace('.', '_');

        string versionFolder = System.IO.Path.Combine(platformFolder, version);
        // Create the version folder if it doesn't exist (mainly for webGL)
        if (!System.IO.Directory.Exists(versionFolder))
        {
            System.IO.Directory.CreateDirectory(versionFolder);
        }

        // Define the build path
        string buildExtension = GetBuildExtension(targetPlatform);
        string buildPath = System.IO.Path.Combine(versionFolder, $"{projectName}_{version}{buildExtension}");

        RHTebug.Log(buildPath);
        // return;
        
        string[] scenes = GetScenes();

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
            target = EditorUserBuildSettings.activeBuildTarget,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);

        if (report.summary.result == BuildResult.Succeeded)
        {
            if(System.IO.Directory.Exists(versionFolder))
            {
                Process.Start("explorer.exe", versionFolder);
            }
            RHTebug.LogSuccess("Build succeeded: " + report.summary.totalSize + " bytes");
        }
        else if (report.summary.result == BuildResult.Failed)
        {
            RHTebug.LogError("Build failed");
        }
    }

    private static string[] GetScenes()
    {
        return EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();
    }

    private static string GetBuildExtension(BuildTarget targetPlatform)
    {
        switch (targetPlatform)
        {
            case BuildTarget.Android:
                // add additional code for development vs release (apk vs adb)
                return ".apk";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return ".exe";
            case BuildTarget.StandaloneOSX:
                return ".app";
            case BuildTarget.WebGL:
                return "";
            default:
                return "";
        }
    }
}