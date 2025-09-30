using System;
using UnityEngine;

/// <summary>
/// Handles anything to do with storing and using items from the inventory
/// </summary>
public class PlayerInventory : PlayerComponent
{
    //todo: yeah, we're gonna need to change up how the inventory handles its events...
    [SerializeField] private int _inventorySize = 25;

    private int _inventorySpace;

    public Interactable[] Inventory {  get; private set; }

    public Action<int> InitialiseInventory;

    #region Initialisation
    private void CreateInventory()
    {
        Inventory = new Interactable[_inventorySize];
        InitialiseInventory?.Invoke(_inventorySize);
        _inventorySpace = _inventorySize;
    }
    public override void InitialiseComponent(PlayerManager playerManager)
    {
        base.InitialiseComponent(playerManager);
        CreateInventory();
    }

    protected override void AddEvents()
    {

    }

    protected override void RemoveEvents()
    {

    }
    #endregion

    public bool AddItem(Interactable item)
    {
        if (_inventorySpace <= 0) return false;
        for (int i = 0; i < _inventorySize; i++)
        {
            if (Inventory[i] == null)
            {
                Inventory[i] = item;
                _inventorySpace--;
                return true;
            }
        }
        return true;
    }

    public bool RemoveItem(Interactable item)
    {
        for (int i = 0; i < _inventorySize; i++)
        {
            if (Inventory[i] == item)
            {
                Inventory[i] = null;

                _inventorySpace++;
                return true;
            }
        }
        return false;
    }

    public Interactable RemoveItemAtIndex(int index)
    {
        Interactable i = Inventory[index];
        Inventory[index] = null;
        if (i != null) _inventorySpace++;
        return i;
    }


}
