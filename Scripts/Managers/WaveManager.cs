using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private float _secondsInNormalWave = 180;
    [SerializeField] private float _secondsInBossWave = 300;
    [SerializeField] private float _secondsMultiplier = 1;


    public WaveState WaveType { get; private set; } = WaveState.Intermission;
    public int WaveNumber { get; private set; } = 0;
    public const int MaxWave = 30;

    private int _bossWave = 5;

    public Action<int> StartWaveE;
    public Action StopWaveE;

    public void StartNewWave()
    {
        if (WaveType != WaveState.Intermission) return;
        WaveNumber++;
        if (WaveNumber % _bossWave == 0)
            WaveType = WaveState.BossWave;
        else
            WaveType = WaveState.NormalWave;

        _secondsPassed = 0;
        StartWaveE?.Invoke(WaveNumber);
    }

    public void StopWave()
    {
        WaveType = WaveState.Intermission;
        StopWaveE?.Invoke();
    }

    private float _secondsPassed = 0;
    private void Update()
    {
        if (WaveType == WaveState.Intermission) return;
        _secondsPassed += Time.deltaTime * _secondsMultiplier;
        if ((WaveType == WaveState.BossWave && _secondsPassed >= _secondsInBossWave) || (WaveType == WaveState.NormalWave && _secondsPassed >= _secondsInNormalWave))
        {
            StopWave();
        }
    }
}

public enum WaveState
{
    Intermission,
    NormalWave,
    BossWave
}
