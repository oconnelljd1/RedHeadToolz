using UnityEngine;
using UnityEngine.SceneManagement;

namespace RedHeadToolz.Tools
{
    public class InitController : MonoBehaviour
    {
        [SerializeField] protected GeneralManager _gm;
        // [SerializeField] protected Scene _sceneToLoad;
        [SerializeField] protected string _sceneToLoad;

        protected virtual void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            // Screen.SetResolution(1920, 1080, false);
        }

        protected virtual void Update()
        {   
            if(_gm.IsInitialized == false) return;

            GeneralManager.Instance.GetManager<SceneManager>().AddScene(_sceneToLoad);
        }
    }
}