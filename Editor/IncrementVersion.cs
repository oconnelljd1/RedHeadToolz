using UnityEditor;
using UnityEngine;
using RedHeadToolz.Debugging;
using System.Linq;
using System;

public static class IncrementVersion
{
    [MenuItem("RedHeadToolz/Version/Print Current Version", false, 0)]
    public static void PrintCurrentVersion()
    {
        RHTebug.Log($"Current Version: {PlayerSettings.bundleVersion}");
    }

    [MenuItem("RedHeadToolz/Version/Increment Major")]
    public static void IncrementMajor()
    {
        VerifyVersion();
        string[] versionParts = PlayerSettings.bundleVersion.Split('.');
        int major = int.Parse(versionParts[0]);
        major++;
        PlayerSettings.bundleVersion = $"{major}.0.0";
        RHTebug.Log($"Version updated to: {PlayerSettings.bundleVersion}");
    }

    [MenuItem("RedHeadToolz/Version/Increment Minor")]
    public static void IncrementMinor()
    {
        VerifyVersion();
        string[] versionParts = PlayerSettings.bundleVersion.Split('.');
        int major = int.Parse(versionParts[0]);
        int minor = int.Parse(versionParts[1]);
        minor++;
        PlayerSettings.bundleVersion = $"{major}.{minor}.0";
        RHTebug.Log($"Version updated to: {PlayerSettings.bundleVersion}");
    }

    [MenuItem("RedHeadToolz/Version/Increment Patch")]
    public static void IncrementPatch()
    {
        VerifyVersion();
        string[] versionParts = PlayerSettings.bundleVersion.Split('.');
        int major = int.Parse(versionParts[0]);
        int minor = int.Parse(versionParts[1]);
        int patch = int.Parse(versionParts[2]);
        patch++;
        PlayerSettings.bundleVersion = $"{major}.{minor}.{patch}";
        RHTebug.Log($"Version updated to: {PlayerSettings.bundleVersion}");
    }

    public static void VerifyVersion()
    {
        if(PlayerSettings.bundleVersion.Split('.').Length == 3) return;

        PlayerSettings.bundleVersion = $"0.0.0";
    }
}