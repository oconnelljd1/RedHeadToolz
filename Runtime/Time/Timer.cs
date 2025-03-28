using System;
using UnityEngine;

namespace RedHeadToolz.Time
{
    public class Timer : BaseManager
    {
        private string _id;
        public string Id => _id;
        private float _duration;
        private float _elapsed;

        private Action _callback;
        
        public void Init(string id, float duration, Action callback = null)
        {
            _id = id;
            _duration = duration;
        }

        public void Update()
        {
            if(_duration == 0) return;

            _elapsed += UnityEngine.Time.deltaTime; // * GeneralManager.Instance.GetManager<TimeManager>().TmeScale;

            if(_elapsed >= _duration)
            {
                _callback?.Invoke();
                _duration = 0;
                _elapsed = 0;
                GeneralManager.Instance.GetManager<TimeManager>().RemoveTimer(_id);
            }
        }
    }
}