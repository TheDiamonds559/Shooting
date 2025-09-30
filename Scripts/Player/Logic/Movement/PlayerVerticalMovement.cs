using UnityEngine;


public class PlayerVerticalMovement : PlayerComponent
{
    [SerializeField] private CharacterController _characterController;


    [Header("User Variables")]
    [SerializeField] private float _groundCheckDistance = .25f;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private float _jumpForce = 20f;

    private bool _groundCheck => Physics.Raycast(
        new(
            _characterController.transform.position.x,
            _characterController.transform.position.y - _characterController.bounds.extents.y,
            _characterController.transform.position.z
            ),
        Vector3.down,
        _groundCheckDistance,
        _groundLayer
        );
    public void CalculateJumps(bool[] jumpInput)
    {
        if (_groundCheck && jumpInput[0])
        {
            _playerManager.GetPlayerComponent<PlayerMovement>().GetVelocity.y = _jumpForce;
        }

        CalculateGravity(ref _playerManager.GetPlayerComponent<PlayerMovement>().GetVelocity);
    }

    public void CalculateGravity(ref Vector3 velocity)
    {
        if (!_groundCheck)
        {
            velocity.y -= _gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    protected override void AddEvents()
    {
        _playerManager.ExtraMovementE += CalculateJumps;
    }

    protected override void RemoveEvents()
    {
        _playerManager.ExtraMovementE -= CalculateJumps;
    }
}
