using UnityEngine;
using System.Collections.Generic;
using RedHeadToolz.Debugging;

namespace RedHeadToolz.Tools
{
    public class InitController : MonoBehaviour
    {
        [SerializeField] protected GeneralManager _gm;
        [SerializeField] protected List<BaseManager> _preloadManagers = new List<BaseManager>();
        // [SerializeField] private List<Scene> initialScenes = new List<Scene>();
        [SerializeField] protected List<string> _preloadScenes = new List<string>();

        // [SerializeField] protected Scene _sceneToLoad;
        [SerializeField] protected string _sceneToLoad;
        protected virtual async void Start()
        {
            RHTebug.Log("InitController Start");
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            // Screen.SetResolution(1920, 1080, false);

            if (_gm == null)
            {
                Debug.LogError("General Manager is not assigned in the InitController.");
                return;
            }
            await _gm.InitializeManagers(_preloadManagers);
            InitializeScenes();
            OnScenesInitialized();
        }

        protected virtual void InitializeScenes()
        {
            RHTebug.Log("InitializeScenes");
            foreach (string scene in _preloadScenes)
            {
                RHTebug.Log($"Loading scene: {scene}");
                GeneralManager.Instance.GetManager<SceneManager>().AddScene(scene);
            }
            // GeneralManager.Instance.GetManager<SceneManager>().AddScene(_sceneToLoad);
        }

        protected virtual void OnScenesInitialized()
        {
            RHTebug.Log("OnScenesInitialized");
            GeneralManager.Instance.GetManager<SceneManager>().AddScene(_sceneToLoad);
        }
    }
}