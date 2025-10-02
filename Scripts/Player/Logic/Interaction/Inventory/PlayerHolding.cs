using System;
using UnityEngine;

public class PlayerHolding : PlayerComponent
{
    [SerializeField] private GameObject _handObject;
    [SerializeField] private Transform _cameraTransform;
    private PlayerHotbar _hotbar;
    private PlayerInteraction _interaction;
    private Interactable _holding;
    private PlayerInput _input;

    public Action<IHoldable> OnHoldEvent;

    public override void InitialiseComponent(PlayerManager playerManager)
    {
        _hotbar = playerManager.GetPlayerComponent<PlayerHotbar>();
        _interaction = playerManager.GetPlayerComponent<PlayerInteraction>();
        _input = playerManager.Input;
        base.InitialiseComponent(playerManager);
    }

    protected override void AddEvents()
    {
        _hotbar.UpdateHotbarE += UpdateHolding;
        _interaction.PickUpE += UpdateHoldingAdapter;
        _playerManager.InteractionE += UseItem;
    }

    protected override void RemoveEvents()
    {
        _hotbar.UpdateHotbarE -= UpdateHolding;
        _interaction.PickUpE -= UpdateHoldingAdapter;
        _playerManager.InteractionE -= UseItem;
    }

    private void UpdateHolding()
    {
        GameObject temp = _holding?.gameObject;
        if (temp != null)
        {
            temp.GetComponent<Renderer>().enabled = false;
            temp.SetActive(false);
            temp.transform.SetParent(null);
        }
        _holding = _hotbar.HotbarStored[_hotbar.HotbarIndex];
        OnHoldEvent?.Invoke(_holding?.GetComponent<IHoldable>());
        if (_holding == null) return;
        if (!_holding.ItemData.IsHoldable) return;

        temp = _holding?.gameObject;
        temp.GetComponent<Renderer>().enabled = true;
        temp.SetActive(true);
        temp.transform.SetParent(_handObject.transform, false);
        temp.transform.localPosition = Vector3.zero;
        temp.transform.localRotation = Quaternion.Euler(_holding.ItemData.HoldingRotation);
    }

    private void UseItem()
    {
        if (_holding == null) return;

        if (_input.RightMouse)
        {
            _holding.SecondaryUse();
        }
        if (_input.LeftMouse)
        {
            _holding.PrimaryUse();
        }
        if (_input.Reload)
        {
            _holding.Reload();
        }
    }

    private void UpdateHoldingAdapter(string t)
    {
        UpdateHolding();
    }
}
