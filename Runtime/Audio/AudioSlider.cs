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
        
        void Start()
        {
            _Audio = GeneralManager.Instance.GetManager<AudioManager>().GetChannel(_channel);
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
            if (_Audio.Muted && volume == 0f) return; 
            
            RHTebug.Log($"SetVolume, volume: {volume}");
            _Audio.SetVolume(volume);
        }
    }
}
