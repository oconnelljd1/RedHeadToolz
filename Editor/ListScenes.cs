using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class ListScenes : EditorWindow
{

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Scenes in Build", EditorStyles.boldLabel);

        GUILayout.BeginArea(new Rect(10, 20, 300, 500));

        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        foreach (EditorBuildSettingsScene scene in scenes)
        {
            if (GUILayout.Button(scene.path, EditorStyles.label))
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