using UnityEngine;
using UnityEngine.SceneManagement;
using RedHeadToolz.Audio;
using RedHeadToolz.Screens;

namespace RedHeadToolz.Tools
{
    public class InitController : MonoBehaviour
    {
        void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            // Screen.SetResolution(1920, 1080, false);
        }

        void Update()
        {
            if (AudioController.Instance == null) return;

            if(ScreenManager.Instance == null) return;

            SceneManager.LoadScene("MainMenu");
        }
    }
}