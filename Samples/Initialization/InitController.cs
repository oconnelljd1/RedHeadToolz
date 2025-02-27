using UnityEngine;
using UnityEngine.SceneManagement;
using RedHeadToolz.Audio;
using RedHeadToolz.Screens;

namespace RedHeadToolz.Tools
{
    public class InitController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            // Screen.SetResolution(1920, 1080, false);
        }

        // Update is called once per frame
        void Update()
        {
            if (AudioController.Instance == null) return;

            if(ScreenManager.Instance == null) return;

            SceneManager.LoadScene("MainMenu");
        }
    }
}