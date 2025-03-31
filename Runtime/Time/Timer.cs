using System;
using UnityEngine;

namespace RedHeadToolz.Time
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private string _id;
        public string Id => _id;
        private float _duration;
        private float _elapsed;
        private bool _running = false;
        private bool _paused = false;
        private bool _loop = false;

        private Action _callback;
        
        public void Init(string id, float duration, Action callback = null)
        {
            _id = id;
            _duration = duration;
            _callback = callback;
        }

        void Update()
        {
            if(_running == false) return;
            if(_duration == 0) return;

            _elapsed += UnityEngine.Time.deltaTime; // * GeneralManager.Instance.GetManager<TimeManager>().TmeScale;

            if(_elapsed >= _duration)
            {
                _callback?.Invoke();
                _elapsed = 0;

                if(_loop == true) return;

                GeneralManager.Instance.GetManager<TimeManager>().RemoveTimer(_id);
            }
        }

        void OnDestroy()
        {
            Debug.Log("test destroy");
        }

        public void Begin() // becasue I can't use Start :(
        {
            _running = true;
            _loop = false;
            _paused = false;
            _elapsed = 0;
        }

        public void Loop()
        {
            _loop = true;
            _running = true;
            _paused = false;
            _elapsed = 0;
        }

        public void Reset()
        {
            _running = false;
            _paused = false;
            _elapsed = 0;
        }

        public void Restart()
        {
            _elapsed = 0;
            _running = true;
            _paused = false;
        }

        public void Stop()
        {
            _running = false;
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Resume()
        {
            _paused = false;
        }
    }
}