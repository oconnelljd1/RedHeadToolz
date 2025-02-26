using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RedHeadToolz.Audio
{
    public class AudioToggle : MonoBehaviour
    {
        [SerializeField] private string _channel;
        [SerializeField] private GameObject _activeSprite;
        [SerializeField] private GameObject _mutedSprite;
        private AudioChannel _Audio;
        private bool _muted = false;
        
        // Start is called before the first frame update
        void Start()
        {
            // do this in start so that Main Menu controller can create the channels in Awake
            _muted = AudioController.Instance.GetChannel(_channel).Muted;
            UpdateSprites();
        }

        private void UpdateSprites()
        {
            _mutedSprite.SetActive(_muted == true);
            _activeSprite.SetActive(_muted == false);
        }

        public void ToggleChannel()
        {
            _muted = !_muted;
            UpdateSprites();
            AudioController.Instance.SetChannelMuted(_channel, _muted);
        }
    }
}
