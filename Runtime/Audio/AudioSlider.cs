using RedHeadToolz.Debugging;
using UnityEngine;
using UnityEngine.UI;

namespace RedHeadToolz.Audio
{
    public class AudioSlider : MonoBehaviour
    {
        [SerializeField] private string _channel;
        [SerializeField] private Slider _slider;
        private AudioChannel _Audio;

        private bool initialized = false;
        
        void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            if(initialized) return;
            initialized = true;

            RHTebug.Log("AudioSlider Start");
            _Audio = GeneralManager.GetManager<AudioManager>().GetChannel(_channel);
            _Audio.VolumeChanged += UpdateVolume;
            _Audio.MuteChanged += UpdateVolume;
            UpdateVolume();
        }


        void OnDestroy()
        {
            _Audio.VolumeChanged -= UpdateVolume;
            _Audio.MuteChanged -= UpdateVolume;
        }

        private void UpdateVolume()
        {
            RHTebug.Log($"UpdateVolume, volume: {_Audio.Volume}, muted: {_Audio.Muted}");
            if (_Audio.Muted)
                _slider.value = 0;
            else
                _slider.value = _Audio.Volume;
        }
        public void SetVolume(float volume)
        {
            Initialize();
            RHTebug.Log("AudioSlider SetVolume");
            if (_Audio.Muted && volume == 0f) return; 
            
            RHTebug.Log($"SetVolume, volume: {volume}");
            _Audio.SetVolume(volume);
        }
    }
}
