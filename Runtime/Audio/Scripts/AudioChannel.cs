using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedHeadToolz.Audio
{

    public class AudioChannel : MonoBehaviour
    {
        public bool Muted => _muted;
        [SerializeField] private GameObject _sourcePrefab;
        private string id;
        public string Id => id;
        List<SoundSource> _sources = new List<SoundSource>();
        int poolSize = 1;
        private float killTime = 10f;
        private bool _muted = false;

        public bool IsPlaying
        {
            get
            {
                foreach(var source in _sources)
                {
                    if(source.Playing) return true;
                }
                return false;
            }
        }

        public void Init(string id, int poolSize = 1, float killTime = 10f)
        {
            this.id = id;
            this.poolSize = poolSize;
            this.killTime = killTime;

            for(int i = 0; i < poolSize; i++)
            {
                // SoundSource source = Instantiate(_sourcePrefab, gameObject.transform).GetComponent<SoundSource>();
                // source.Init(killTime);
                // _sources.Add(source);
                SpawnSound().Init(killTime);
            }
        }

        private SoundSource GetSource()
        {
            SoundSource toPlay = null;
            if(poolSize == 1)
                toPlay = _sources[0];

            foreach(var sound in _sources)
            {
                if(sound == null) continue;
                if(sound.Playing) continue;

                toPlay = sound;
                break;
            }
            
            if(toPlay == null) toPlay = SpawnSound();

            return toPlay;
        }

        public void Play(string clip)
        {
            Play(AudioController.Instance.GetClip(clip));
        }

        public void Play(string clip, Action callback)
        {
            Play(AudioController.Instance.GetClip(clip), callback);
        }

        public void Play(AudioClip clip)
        {
            GetSource().Play(clip);
        }

        public void Play(AudioClip clip, Action callback)
        {
            GetSource().Play(clip);

            if(_muted)
            {
                callback();
                return;
            }

            var callBack = Callback(clip.length, callback);
            StartCoroutine(callBack);
        }

        private IEnumerator Callback(float dur, Action callback)
        {
            yield return new WaitForSeconds(dur);
            callback();
        }

        public void Loop(string clip)
        {
            Loop(AudioController.Instance.GetClip(clip));
        }

        public void Loop(AudioClip clip)
        {
            GetSource().Loop(clip);
        }

        public void Stop()
        {
            foreach(var source in _sources)
            {
                source.Stop();
            }
            StopAllCoroutines();
        }

        private SoundSource SpawnSound()
        {
            SoundSource sound = Instantiate(_sourcePrefab, transform).GetComponent<SoundSource>();
            _sources.Add(sound);
            return sound;
        }

        public void Mute()
        {
            _muted = true;
            foreach(var source in _sources)
            {
                source.Mute();
            }
        }

        public void Unmute()
        {
            _muted = false;
            foreach(var source in _sources)
            {
                source.Unmute();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(_sources.Count > poolSize)
            {
                for(int i = _sources.Count -1; i > -1; i--)
                {
                    _sources[i].Tick();
                    if(_sources[i].Expired)
                    {
                        Destroy(_sources[i].gameObject);
                        _sources.RemoveAt(i);
                    }
                }
            }
        }
    }
}