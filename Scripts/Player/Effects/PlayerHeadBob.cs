using UnityEngine;

public class PlayerHeadBob : PlayerComponent
{
    [SerializeField] private GameObject _cameraObject;
    [SerializeField] private float _amplitude = .1f;
    [SerializeField] private float _speed = 5f;

    private float time = 0;

    protected override void AddEvents()
    {
        
    }

    protected override void RemoveEvents()
    {
        
    }

    void Update()
    {
        if (_playerManager.PlayerLocked) return;
        if (_playerManager.Input.MoveInput.sqrMagnitude > 0)
        {
            time += Time.deltaTime;
            _cameraObject.transform.localPosition = new Vector3(0, Mathf.Abs(Mathf.Sin(time * _speed) * _amplitude), 0);
        }
        else
        {
            time = 0;
        }
    }
}
