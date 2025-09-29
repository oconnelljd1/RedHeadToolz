using System.Collections.Generic;
using RedHeadToolz;
using RedHeadToolz.Debugging;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Threading.Tasks;

namespace RedHeadToolz
{
    public class SceneManager : BaseManager
    {
        protected List<string> activeScenes = new List<string>();

        public Action<string> OnSceneLoaded;
        public Action<string> OnSceneUnloaded;

        public override async Task<InitializationStatus> Init()
        {
            activeScenes = new List<string> { UnityEngine.SceneManagement.SceneManager.GetActiveScene().name };

            return await base.Init();
        }

        public void LoadScene(string sceneName)
        {
            RHTebug.Log($"Loading scene: {sceneName}");
            if (string.IsNullOrEmpty(sceneName))
            {
                RHTebug.LogError($"Tried to load a scene with an empty or null name!");
                return;
            }
            
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            activeScenes = new List<string> { sceneName };
            OnSceneLoaded?.Invoke(sceneName);
        }

        public void AddScene(string sceneName, bool forceLoad = false)
        {
            RHTebug.Log($"Adding scene: {sceneName}");
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
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            activeScenes.Add(sceneName);
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