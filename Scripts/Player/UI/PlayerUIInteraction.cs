using TMPro;
using UnityEngine;

public class PlayerUIInteraction : PlayerUIComponent
{
    [SerializeField] private TMP_Text _interactionText;
    [SerializeField] private TMP_Text _pickUpText;

    private PlayerInteraction _interaction;

    #region Initialisation
    public override void InitialiseComponent(PlayerUIManager playerUIManager)
    {
        _interaction = playerUIManager.PlayerManager.GetPlayerComponent<PlayerInteraction>();
        base.InitialiseComponent(playerUIManager);
        ClearCrosshairText();
        ClearPickUpText();
    }

    protected override void AddEvents()
    {
        _interaction.HoverOverInteraction += HoveringInteractable;
        _interaction.StopHoverOverInteraction += ClearPickUpText;
        _interaction.StopHoverOverInteraction += ClearCrosshairText;
        _interaction.OnPickUpHover += CanPickUp;
    }

    protected override void RemoveEvents()
    {
        _interaction.HoverOverInteraction -= HoveringInteractable;
        _interaction.StopHoverOverInteraction -= ClearCrosshairText;
        _interaction.StopHoverOverInteraction -= ClearPickUpText;
        _interaction.OnPickUpHover -= CanPickUp;
    }
    #endregion

    public void HoveringInteractable(string name)
    {
        _interactionText.text = $"{name}";
    }

    public void CanPickUp(string name)
    {
        _pickUpText.text = $"E - pickup {name}";
    }

    public void ClearPickUpText()
    {
        _pickUpText.text = "";
    }

    public void ClearCrosshairText()
    {
        _interactionText.text = "";
    }


}
