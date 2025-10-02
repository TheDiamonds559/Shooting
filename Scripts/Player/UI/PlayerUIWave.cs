using TMPro;
using UnityEngine;

public class PlayerUIWave : PlayerUIComponent
{
    [SerializeField] private TMP_Text _waveText;

    
    protected override void AddEvents()
    {
        GameManager.Instance.WaveManager.StartWaveE += UpdateWave;
        GameManager.Instance.WaveManager.StopWaveE += UpdateWave;
    }

    protected override void RemoveEvents()
    {
        GameManager.Instance.WaveManager.StartWaveE -= UpdateWave;
        GameManager.Instance.WaveManager.StopWaveE -= UpdateWave;
    }

    private void UpdateWave()
    {
        _waveText.text = "Intermission";
    }

    private void UpdateWave(int waveNumber)
    {
        _waveText.text = $"Wave {waveNumber}/30";
    }
}
