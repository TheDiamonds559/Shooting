using TMPro;
using UnityEngine;

public class PlayerUIHoldingInfo : PlayerUIComponent
{
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _description;

    private IHoldable _currentItemHolding;
    protected override void AddEvents()
    {
        ClearText();
        _manager.PlayerManager.GetPlayerComponent<PlayerHolding>().OnHoldEvent += UpdateHolding;
    }

    protected override void RemoveEvents()
    {
        _manager.PlayerManager.GetPlayerComponent<PlayerHolding>().OnHoldEvent -= UpdateHolding;
    }

    private void UpdateHolding(IHoldable holdable)
    {
        _currentItemHolding = holdable;
        if (holdable == null)
        {
            ClearText();
            return;
        }
        _itemName.text = holdable.GetName();
        _description.text = holdable.GetInformation();
    }

    private void ClearText()
    {
        _itemName.text = string.Empty;
        _description.text = string.Empty;
    }

    private void Update()
    {
        if (_currentItemHolding == null) return;
        else
        {
            _description.text = _currentItemHolding.GetInformation();
        }
    }
}
