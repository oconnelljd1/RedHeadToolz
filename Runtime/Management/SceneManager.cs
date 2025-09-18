using System.Collections.Generic;
using RedHeadToolz;
using RedHeadToolz.Debugging;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

namespace RedHeadToolz
{
    
    public class SceneManager : BaseManager
    {
        // [SerializeField] private List<Scene> initialScenes = new List<Scene>();
        [SerializeField] private List<string> initialScenes = new List<string>();
        private List<string> activeScenes = new List<string>();

        public Action<string> OnSceneLoaded;
        public Action<string> OnSceneUnloaded;

        public override void Init()
        {
            // foreach (Scene scene in initialScenes)
            foreach (string scene in initialScenes)
            {
                AddScene(scene);
            }
            base.Init();
        }

        public void LoadScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            activeScenes = new List<string> { sceneName };
            OnSceneLoaded?.Invoke(sceneName);
        }

        public void AddScene(string sceneName, bool forceLoad = false)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                RHTebug.LogError($"Tried to load a scene with an empty or null name!");
                return;
            }
            if (forceLoad == false && activeScenes.Contains(sceneName))
            {
                RHTebug.LogWarning($"Tried to load a scene that is already loaded: {sceneName}");
                return;
            }
            activeScenes.Add(sceneName);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            OnSceneLoaded?.Invoke(sceneName);
        }

        // public void AddScene(Scene scene, bool forceLoad = false)
        // {
        //     if (scene == null)
        //     {
        //         RHTebug.LogError($"Tried to load a null scene!");
        //         return;
        //     }
        //     AddScene(scene.name, forceLoad);
        // }

        public void RemoveScene(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                RHTebug.LogError($"Tried to load a scene with an empty or null name!");
                return;
            }
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
            activeScenes.Remove(sceneName);
        }

        // public void RemoveScene(Scene scene)
        // {
        //     if (scene == null)
        //     {
        //         RHTebug.LogError($"Tried to unload a null scene!");
        //         return;
        //     }
        //     RemoveScene(scene.name);
        // }
    }
}