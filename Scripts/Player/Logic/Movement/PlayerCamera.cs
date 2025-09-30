using UnityEngine;

public class PlayerCamera : PlayerCameraBase
{
    [Header("User Variables")]
    [SerializeField] private float _cameraSensitivity = 50;
    [SerializeField] private Vector2 _cameraExtents = new(-60, 90);

    private float _xRotation = 0;

    protected override void MoveCamera(Vector2 inputDir)
    {
        float xPos = inputDir.x * _cameraSensitivity * Time.deltaTime;
        float yPos = inputDir.y * _cameraSensitivity * Time.deltaTime;

        _xRotation -= yPos;
        _xRotation = Mathf.Clamp(_xRotation, _cameraExtents.x, _cameraExtents.y);

        Camera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        _characterController.transform.Rotate(Vector3.up * xPos);
    }
}
