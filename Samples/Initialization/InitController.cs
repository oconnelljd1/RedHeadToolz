using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RedHeadToolz.Audio;

namespace RedHeadToolz.Utils
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

            SceneManager.LoadScene("MainMenu");
        }
    }
}