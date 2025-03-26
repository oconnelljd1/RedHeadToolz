using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor.ShortcutManagement;

public class ListScenes : EditorWindow
{
    [Shortcut("RedHeadToolz/Open List Scenes", KeyCode.T, ShortcutModifiers.Action)]
    public static void CheckKeys()
    {
        GetWindow<ListScenes>("Scenes in Build");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Scenes in Build", EditorStyles.boldLabel);

        GUILayout.BeginArea(new Rect(10, 20, 300, 500));

        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        foreach (EditorBuildSettingsScene scene in scenes)
        {
            // can add more code to show more of the path if two scenes have the same name
            // can add more code to exclude current scene
            string[] pathParts = scene.path.Split('/');
            string sceneName = pathParts[pathParts.Length - 1].Replace(".unity", "");
            if (GUILayout.Button(sceneName, EditorStyles.label))
            {
                OpenScene(scene.path);
                Close();
            }
        }

        GUILayout.EndArea();
    }

    private void OpenScene(string scenePath)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(scenePath);
        }
    }
}