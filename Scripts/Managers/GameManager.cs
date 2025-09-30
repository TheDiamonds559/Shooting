using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Interactable gun;


    private void Start()
    {
        Interactable g = Instantiate(gun);
        g.AttachPlayer(playerManager);
        playerManager.GetPlayerComponent<PlayerInventoryManager>().AddItem(g);
    }
}
