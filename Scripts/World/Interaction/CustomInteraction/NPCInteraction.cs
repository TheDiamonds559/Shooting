using JustJJ.DataStructures;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteract
{
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private GameObject _face;

    private DialogueGraph _dialogueGraph;

    private void Start()
    {
        _dialogueGraph = DialogueToGraph.DialogueScriptToGraph(_dialogue);
    }

    public void Interact(PlayerManager interactedPlayer)
    {
        interactedPlayer.GetPlayerComponent<PlayerDialogue>().CallNewDialogue(_dialogueGraph);
        transform.LookAt(interactedPlayer.transform.position);
        interactedPlayer.GetPlayerComponent<PlayerCamera>().Camera.transform.LookAt(_face.transform);
    }
}
