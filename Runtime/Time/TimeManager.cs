using System;
using System.Collections.Generic;
using RedHeadToolz;
using RedHeadToolz.Debugging;
using UnityEngine;

namespace RedHeadToolz.Time
{
    public class TimeManager : BaseManager
    {
        [SerializeField] private GameObject _timerPrefab;
        private float _timeScale = 1f;
        public float TimeScale => _paused? 0f : _timeScale;

        private bool _paused = false;
        public bool Paused => _paused;

        private List<Timer> _timers = new List<Timer>();


        public Timer AddTimer(string id, float duration, Action callback = null)
        {
            var oldTimer = _timers.Find(x=>x.Id == id);
            if(oldTimer)
            {
                Debug.LogWarning($"Timer {id} already exists.");
                return oldTimer;
            }

            Timer timer = CreateTimer();
            timer.Init(id, duration, callback);
            return timer;
        }

        private Timer CreateTimer()
        {
            Timer timer = Instantiate(_timerPrefab, transform).GetComponent<Timer>();
            _timers.Add(timer);
            return timer;
        }

        public Timer GetTimer(string id)
        {
            var timer = _timers.Find(x=>x.Id == id);
            if(timer == null)
            {
                RHTebug.LogError($"No timer {id} found");
            }
            return timer;
        }

        public void RemoveTimer(Timer timer)
        {
            if(_timers.Contains(timer))
            {
                _timers.Remove(timer);
                Destroy(timer.gameObject);
            }
        }

        public void RemoveTimer(string id)
        {
            Timer timer = _timers.Find(x=>x.Id == id);
            if(timer != null)
            {
                _timers.Remove(timer);
                Destroy(timer.gameObject);
            }
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Unpause()
        {
            _paused = false;
        }

        public void SetPaused(bool pause)
        {
            _paused = pause;
        }
    }
}