using System.Collections;
using System.Collections.Generic;
using RedHeadToolz.Debugging;
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
        
        void Start()
        {
            _Audio = GeneralManager.GetManager<AudioManager>().GetChannel(_channel);
            _Audio.MuteChanged += UpdateMuted;
            UpdateMuted();
        }

        void OnDestroy()
        {
            _Audio.MuteChanged -= UpdateMuted;
        }

        private void UpdateMuted()
        {
            RHTebug.Log($"UpdateMuted, muted: {_Audio.Muted}");
            _muted = _Audio.Muted;
            UpdateSprites();
        }

        private void UpdateSprites()
        {
            _mutedSprite.SetActive(_muted == true);
            _activeSprite.SetActive(_muted == false);
        }

        public void ToggleChannel()
        {
            _Audio.ToggleMuted();
        }
    }
}
