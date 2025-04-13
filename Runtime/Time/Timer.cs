using System;
using RedHeadToolz.Debugging;
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
        public bool Running => _running;
        private bool _paused = false;
        public bool Paused => _paused;
        private bool _loop = false;

        private Action _callback;
        
        public Timer Init(string id, float duration, Action callback = null)
        {
            _id = id;
            _duration = duration;
            _callback = callback;

            return this;
        }

        void Update()
        {
            if(_running == false) return;
            if(_paused == true) return;
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
            RHTebug.Log($"{_id} Begin");
            _running = true;
            _loop = false;
            _paused = false;
            _elapsed = 0;
        }

        public void Loop()
        {
            RHTebug.Log($"{_id} Loop");
            _loop = true;
            _running = true;
            _paused = false;
            _elapsed = 0;
        }

        public void Reset()
        {
            RHTebug.Log($"{_id} Reset");
            _running = false;
            _paused = false;
            _elapsed = 0;
        }

        public void Restart()
        {
            RHTebug.Log($"{_id} Restart");
            _elapsed = 0;
            _running = true;
            _paused = false;
        }

        public void Stop()
        {
            RHTebug.Log($"{_id} Stop");
            _running = false;
        }

        public void Pause()
        {
            RHTebug.Log($"{_id} Pause");
            _paused = true;
        }

        public void Resume()
        {
            RHTebug.Log($"{_id} Resume");
            _paused = false;
        }
    }
}