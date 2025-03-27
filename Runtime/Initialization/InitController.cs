using UnityEngine;
using UnityEngine.SceneManagement;
using RedHeadToolz.Audio;
using RedHeadToolz.Screens;

namespace RedHeadToolz.Tools
{
    public class InitController : MonoBehaviour
    {
        [SerializeField] GeneralManager _gm;
        [SerializeField] private string _sceneToLoad = "MainMenu";
        void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            // Screen.SetResolution(1920, 1080, false);
        }

        void Update()
        {
            // move thesse two into managers
            if (AudioController.Instance == null) return;

            if(ScreenManager.Instance == null) return;
            
            if(_gm.IsInitialized == false) return;

            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}