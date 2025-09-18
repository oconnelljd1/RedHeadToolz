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
            // do this in start so that Main Menu controller can create the channels in Awake
            _muted = GeneralManager.Instance.GetManager<AudioManager>().GetChannel(_channel).Muted;
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
            GeneralManager.Instance.GetManager<AudioManager>().SetChannelMuted(_channel, _muted);
        }
    }
}
