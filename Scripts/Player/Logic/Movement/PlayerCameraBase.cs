using UnityEngine;

public abstract class PlayerCameraBase : PlayerComponent
{
    [field: SerializeField] public GameObject Camera { get; private set; }
    [SerializeField] protected GameObject _characterController;

    protected abstract void MoveCamera(Vector2 inputDir);

    protected override void AddEvents()
    {
        _playerManager.CameraMovementE += MoveCamera;
    }

    protected override void RemoveEvents()
    {
        _playerManager.CameraMovementE -= MoveCamera;
    }
}
