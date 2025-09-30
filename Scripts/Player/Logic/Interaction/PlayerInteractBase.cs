using System;
using UnityEngine;

public abstract class PlayerInteractBase : PlayerComponent
{
    [SerializeField] protected GameObject _camera;
    [SerializeField] protected float _playerInteractionDistance = 5.0f;

    protected PlayerInput _input;

    public Action<string> HoverOverInteraction { get; set; }
    public Action StopHoverOverInteraction { get; set; }

    public override void InitialiseComponent(PlayerManager playerManager)
    {
        base.InitialiseComponent(playerManager);
        _input = _playerManager.Input;
    }

    protected override void AddEvents()
    {
        _playerManager.InteractionE += UpdateInteractions;
    }

    protected override void RemoveEvents()
    {
        _playerManager.InteractionE -= UpdateInteractions;
    }

    protected abstract void UpdateInteractions();
}
