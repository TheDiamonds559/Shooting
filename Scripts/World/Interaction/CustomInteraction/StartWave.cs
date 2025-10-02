using UnityEngine;

public class StartWave : MonoBehaviour, IInteract
{
    public void Interact(PlayerManager interactedPlayer)
    {
        GameManager.Instance.WaveManager.StartNewWave();
    }
}
