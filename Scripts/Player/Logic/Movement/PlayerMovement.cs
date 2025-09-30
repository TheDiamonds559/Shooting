using UnityEngine;

public class PlayerMovement : PlayerComponent
{
    [SerializeField]
    private CharacterController _characterController;

    [Header("User Variables")]
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _sprintMultiplier = 2.0f;

    private Vector3 _velocity = Vector3.zero;

    private void CalculateMovements(Vector2 inputDir)
    {
        Vector2 normal = inputDir.normalized;
        float prevYVel = _velocity.y;

        _velocity = normal.x * transform.right + normal.y * transform.forward;
        _velocity *= _speed;

        _velocity.y = prevYVel;
    }

    private void FinaliseMovements()
    {
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void SprintMovement(bool[] sprintInput)
    {
        if (sprintInput[1])
        {
            float prevYVel = _velocity.y;
            _velocity *= _sprintMultiplier;
            _velocity.y = prevYVel;
        }
    }

    protected override void AddEvents()
    {
        _playerManager.InitialMovementE += CalculateMovements;
        _playerManager.FinalMovementE += FinaliseMovements;
        _playerManager.ExtraMovementE += SprintMovement;
    }

    protected override void RemoveEvents()
    {
        _playerManager.InitialMovementE -= CalculateMovements;
        _playerManager.FinalMovementE -= FinaliseMovements;
        _playerManager.ExtraMovementE -= SprintMovement;
    }

    public ref Vector3 GetVelocity => ref _velocity;
}
