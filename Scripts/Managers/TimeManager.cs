using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Action OnTickEvent;

    [SerializeField] private float _secondsPerTick = 5.0f;

    private WaveManager _wm;

    private float _secondsPassed = 0.0f;

    private void Start()
    {
        _wm = GameManager.Instance.WaveManager;
    }

    private void Update()
    {
        if (_wm.WaveType == WaveState.Intermission)
        {
            _secondsPassed = 0;
            return;
        }
        _secondsPassed += Time.deltaTime;
        if (_secondsPassed >= _secondsPerTick)
        {
            OnTickEvent?.Invoke();
            _secondsPassed = 0.0f;
        }
    }
}
