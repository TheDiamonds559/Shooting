using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHotbar : PlayerComponent
{
    [field:SerializeField] public int HotbarSize { get; private set; }

    public int HotbarIndex { get; private set; }
    public Interactable[] HotbarStored {  get; private set; }

    private PlayerInput _input;

    private int _hotbarSpaceLeft;

    #region Events
    public Action<bool> HotbarVisibilityE;
    public Action UpdateHotbarE;
    #endregion

    #region Initialisation
    public override void InitialiseComponent(PlayerManager playerManager)
    {
        base.InitialiseComponent(playerManager);
        HotbarInitialisation();
    }

    private void HotbarInitialisation()
    {
        _input = _playerManager.Input;
        HotbarStored = new Interactable[HotbarSize];
        _hotbarSpaceLeft = HotbarSize;
    }

    protected override void AddEvents()
    {
        _playerManager.InventoryE += HotbarFuntion;
    }

    protected override void RemoveEvents()
    {
        _playerManager.InventoryE -= HotbarFuntion;
    }
    #endregion

    #region Scrolling
    private void ScrollHotbar()
    {
        if (_input.MouseScrollDown)
        {
            ScrollLeft();
            UpdateHotbarE?.Invoke();
        }
        else if (_input.MouseScrollUp)
        {
            ScrollRight();
            UpdateHotbarE?.Invoke();
        }
    }

    private void ScrollLeft()
    {
        HotbarIndex--;
        if (HotbarIndex < 0)
            HotbarIndex = HotbarSize-1;
    }

    private void ScrollRight()
    {
        HotbarIndex++;
        if (HotbarIndex >= HotbarSize)
            HotbarIndex = 0;
    }
    #endregion

    private void HotbarFuntion()
    {
        ScrollHotbar();
    }

    public bool AddItem(Interactable item)
    {
        if (IsHotbarFull()) return false;
        for (int i = 0; i < HotbarSize; i++)
        {
            if (HotbarStored[i] == null)
            {
                HotbarStored[i] = item;
                HotbarIndex = i;
                _hotbarSpaceLeft--;
                UpdateHotbarE?.Invoke();
                return true;
            }
        }
        return true;
    }

    public bool RemoveItem(Interactable item)
    {
        for (int i = 0; i < HotbarSize;i++)
        {
            if (HotbarStored[i] == item)
            {
                HotbarStored[i] = null;

                _hotbarSpaceLeft++;
                ScrollLeft();
                UpdateHotbarE?.Invoke();
                return true;
            }
        }
        return false;
    }

    public Interactable RemoveItemAtIndex(int index)
    {
        Interactable i = HotbarStored[index];
        HotbarStored[index] = null;
        if (i != null) _hotbarSpaceLeft++;
        UpdateHotbarE?.Invoke();
        return i;
    }

    public bool IsHotbarFull()
    {
        return _hotbarSpaceLeft <= 0;
    }
}
