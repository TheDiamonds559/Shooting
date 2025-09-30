using System;
using UnityEngine;

/// <summary>
/// Handles any form of interaction
/// </summary>
public class PlayerInteraction : PlayerInteractBase
{

    private Interactable _hoveringInteractable;

    public Action<Dialogue> OnInteractionDialogue;
    public Action<Dialogue, int> OnInteractionDialogueSingle;
    public Action<string> OnPickUpHover;
    public Action<string> PickUpE;

    private int _tick = 0;

    protected override void UpdateInteractions()
    {
        if (_hoveringInteractable != null)
        {
            Interact();
            PickUp();
        }
        _tick++;
        if (_tick % 10 != 0) return;
        CheckHovering();
        _tick = 0;
    }

    private void CheckHovering()
    {
        RaycastHit hit;
        if (Physics.SphereCast(_camera.transform.position, .1f,_camera.transform.forward, out hit, _playerInteractionDistance))
        {
            GameObject gameObject = hit.collider.gameObject;
            if (gameObject.TryGetComponent<Interactable>(out _hoveringInteractable))
            {
                HoverOverInteraction?.Invoke(_hoveringInteractable.ItemName);
                return;
            }
        }
        NotHovering();
    }

    private void Interact()
    {
        if (_input.InteractButton)
        {
            _hoveringInteractable.Interact(_playerManager);
        }
    }

    private bool PickUp()
    {
        if (_hoveringInteractable.CanPickUp)
        {
            OnPickUpHover?.Invoke(_hoveringInteractable.ItemName);
        }
        else return false;
        if (_input.PickUpButton)
        {
            if (_hoveringInteractable.Pickup(_playerManager))
            {
                PickUpE?.Invoke(_hoveringInteractable.ItemName);
                return true;
            }
        }
        return false;
    }

    private void NotHovering()
    {
        StopHoverOverInteraction?.Invoke();
        _hoveringInteractable = null;
    }

    public Interactable GetHovering()
    {
        return _hoveringInteractable;
    }
}
