using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : MonoBehaviour
{
    #region Core Variables
    private PlayerComponent[] _playerComponents;

    public Action<Vector2> CameraMovementE;
    public Action<Vector2> InitialMovementE;
    public Action<bool[]> ExtraMovementE;
    public Action FinalMovementE;

    public PlayerInput Input { get; private set; }

    public bool PlayerLocked { get; private set; }
    #endregion

    #region Extra Variables
    public Action InteractionE;
    public Action InventoryE;
    public Action DialogueE;

    #endregion

    private void Awake()
    {
        _playerComponents = GetComponentsInChildren<PlayerComponent>();
        Input = GetComponent<PlayerInput>();

        SetCursorLocked(true);

        foreach (PlayerComponent component in _playerComponents)
        {
            component.InitialiseComponent(this);
        }
    }

    private void Update()
    {
        if (!PlayerLocked)
        {
            UpdateMovements();

            Input.ReadInteractionInputs();

            InteractionE?.Invoke();
            InventoryE?.Invoke();
        }

        Input.ReadDialogueInputs();
        DialogueE?.Invoke();
    }

    private void UpdateMovements()
    {
        Input.ReadMovementInputs();

        CameraMovementE?.Invoke(Input.CameraInput);
        InitialMovementE?.Invoke(Input.MoveInput);

        ExtraMovementE?.Invoke(new bool[] { Input.JumpInput, Input.SprintInput, Input.CrouchInput });

        FinalMovementE?.Invoke();
    }

    public void SetPlayerLockState(bool lockState)
    {
        PlayerLocked = lockState;
    }

    public T GetPlayerComponent<T>() where T : PlayerComponent
    {
        return _playerComponents.OfType<T>().FirstOrDefault();
    }

    public void SetCursorLocked(bool locked)
    {
        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    #region Cleanup
    private void UninitialiseComponents()
    {
        foreach (PlayerComponent component in _playerComponents)
        {
            component.UninitialiseComponent();
        }
    }

    private void OnDestroy()
    {
        UninitialiseComponents();
    }
    #endregion
}
