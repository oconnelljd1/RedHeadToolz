using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedHeadToolz.Audio
{
    public class SoundSource : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        private float killTime = 0f;
        private float currentTime = 0f;
        public bool Playing => source.isPlaying;
        public AudioClip Clip => source.clip;
        public bool Expired => currentTime > killTime;

        public void Init(float kTime = 0f)
        {
            killTime = kTime;
        }

        public void Play(AudioClip clip)
        {
            source.Stop();
            currentTime = 0;
            source.clip = clip;
            source.loop = false;
            source.Play();
        }

        public void Loop(AudioClip clip)
        {
            source.Stop();
            currentTime = 0;
            source.clip = clip;
            source.loop = true;
            source.Play();
        }

        public void Stop()
        {
            source.Stop();
        }

        public void Tick()
        {
            if(Playing)return;

            currentTime += Time.deltaTime;
            // if(currentTime > killTime)
        }

        public void Mute()
        {
            source.volume = 0;
        }

        public void Unmute()
        {
            source.volume = 1;
        }
    }
}
