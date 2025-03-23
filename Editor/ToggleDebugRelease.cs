using System.Collections.Generic;
using UnityEngine;
using RedHeadToolz.Debugging;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;

public class ToggleDebugRelease : MonoBehaviour
{
    private const string DEVELOP_KEY = "DEVELOP";
    private const string RELEASE_KEY = "RELEASE";

    private static List<NamedBuildTarget> relevantBuildTargets = new List<NamedBuildTarget> 
    {
        NamedBuildTarget.Android, 
        NamedBuildTarget.iOS,
        NamedBuildTarget.WebGL,
        NamedBuildTarget.Standalone
    };

#if DEVELOP
    [MenuItem("RedHeadToolz/Build/Set Release", false, 0)]
#else
    [MenuItem("RedHeadToolz/Build/Set Development", false, 0)]
#endif
	private static void ToggleDevelopRelease()
	{
        foreach (NamedBuildTarget targetGroup in relevantBuildTargets)
        {
            string[] defines = new string[0];
            PlayerSettings.GetScriptingDefineSymbols(targetGroup, out defines);
            List<string> debugSymbols = defines.ToList();
            if(debugSymbols.Contains(DEVELOP_KEY))
            {
                debugSymbols.Remove(DEVELOP_KEY);
                debugSymbols.Add(RELEASE_KEY);
            }
            else // to set develop if we aren't in either mode
            {
                debugSymbols.Remove(RELEASE_KEY);
                debugSymbols.Add(DEVELOP_KEY);
            }
            PlayerSettings.SetScriptingDefineSymbols(targetGroup, debugSymbols.ToArray());
        }
	}

    [MenuItem("RedHeadToolz/Build/Clear Build Defines", false, 0)]
    private static void ClearBuildDefines()
    {
        foreach (NamedBuildTarget targetGroup in relevantBuildTargets)
        {
            string[] defines = new string[0];
            PlayerSettings.GetScriptingDefineSymbols(targetGroup, out defines);
            List<string> debugSymbols = defines.ToList();

            debugSymbols.Remove(RELEASE_KEY);
            debugSymbols.Remove(DEVELOP_KEY);

            PlayerSettings.SetScriptingDefineSymbols(targetGroup, debugSymbols.ToArray());
        }
    }
}