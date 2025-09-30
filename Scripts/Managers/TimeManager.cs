using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Action OnTickEvent;

    [SerializeField] private float _secondsPerTick = 5.0f;

    private float _secondsPassed = 0.0f;

    private void Update()
    {
        _secondsPassed += Time.deltaTime;
        if (_secondsPassed >= _secondsPerTick)
        {
            OnTickEvent?.Invoke();
            _secondsPassed = 0.0f;
        }
    }
}
