using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [field:SerializeField] public string ItemName { get; private set; }
    [field: SerializeField] public ItemData ItemData { get; private set; } 

    private IInteract _interactBehaviour;
    private IPickUp _pickupBehaviour;
    private IUseable _useableBehaviour;

    public PlayerManager AttachedPlayer { get; private set; }

    public bool CanPickUp { get; private set; }
    public Action PickedUpE;

    private void Awake()
    {
        _interactBehaviour = GetComponent<IInteract>();
        _pickupBehaviour = GetComponent<IPickUp>();
        _useableBehaviour = GetComponent<IUseable>();
        if (_pickupBehaviour != null) CanPickUp = true;
        else CanPickUp = false;
    }

    public void Interact(PlayerManager interactedPlayer)
    {
        _interactBehaviour?.Interact(interactedPlayer);
    }

    public bool Pickup(PlayerManager interactedPlayer)
    {
        if (_pickupBehaviour == null) return false;
       
        if (_pickupBehaviour.Pickup(interactedPlayer, this))
        {
            AttachedPlayer = interactedPlayer;
            return true;
        }
        return false;
    }

    public void AttachPlayer(PlayerManager interactedPlayer)
    {
        AttachedPlayer = interactedPlayer;
        PickedUpE?.Invoke();
    }

    public void Drop()
    {
        _pickupBehaviour?.DropItem();
    }

    public void PrimaryUse()
    {
        _useableBehaviour?.PrimaryUse();
    }

    public void SecondaryUse()
    {
        _useableBehaviour?.SecondaryUse();
    }
}
