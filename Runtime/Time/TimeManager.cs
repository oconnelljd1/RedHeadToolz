using System;
using System.Collections.Generic;
using RedHeadToolz;
using UnityEngine;

public class TimeManager : BaseManager
{
    [SerializeField] private GameObject _timerPrefab;
    private float _timeScale = 1f;
    public float TimeScale => _paused? 0f : _timeScale;

    private bool _paused = false;
    public bool Paused => _paused;

    private List<Timer> _timers = new List<Timer>();


    public void StartTimer(string id, float duration, Action callback = null)
    {
        Timer timer = CreateTimer();
        timer.Init(id, duration, callback);
    }

    private Timer CreateTimer()
    {
        Timer timer = Instantiate(_timerPrefab, transform).GetComponent<Timer>();
        _timers.Add(timer);
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
            Destroy(timer);
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