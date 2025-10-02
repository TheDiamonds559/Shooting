using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public PlayerManager playerManager;
    [SerializeField] private Interactable gun;
    [field:SerializeField] public TimeManager TimeManager {  get; private set; }
    [field:SerializeField] public WaveManager WaveManager { get; private set; }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        Interactable g = Instantiate(gun);
        g.AttachPlayer(playerManager);
        playerManager.GetPlayerComponent<PlayerInventoryManager>().AddItem(g);
    }
}
