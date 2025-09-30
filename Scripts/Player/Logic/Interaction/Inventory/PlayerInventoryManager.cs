using UnityEngine;

public class PlayerInventoryManager : PlayerComponent
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerHotbar _hotbar;

    private bool _hasHotbar, _hasInventory;

    private void Awake()
    {
        if (_inventory != null) _hasInventory = true;
        if (_hotbar != null) _hasHotbar = true;
    }

    protected override void AddEvents()
    {
        
    }

    protected override void RemoveEvents()
    {
        
    }

    public bool AddItem(Interactable item)
    {
        if (_hasHotbar)
        {
            if (_hotbar.AddItem(item)) return true;
        }
        if (_hasInventory)
        {
            if (_inventory.AddItem(item)) return true;
        }
        return false;
    }
}
