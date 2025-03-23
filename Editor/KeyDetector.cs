using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
class MyKeyDetector
{
    static MyKeyDetector ()
    {
        SceneView.duringSceneGui += CheckKeys;
    }
    
    public static void CheckKeys(SceneView view)
    {
        Event current = Event.current;
        if (current.type == EventType.KeyDown && current.modifiers == EventModifiers.Control && current.keyCode == KeyCode.T)
        {
            // Debug.Log("control T pressed!");
            EditorWindow.GetWindow<ListScenes>("Scenes in Build");
        }
    }
}