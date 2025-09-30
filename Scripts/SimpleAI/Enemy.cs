using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [field:SerializeField] public GameObject Target { get; set; }
    private Rigidbody _rb;

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _pathRadius = 5.0f;
    [SerializeField] private Quaternion _normalRotation;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Target == null) return;
        if (Vector3.Distance(transform.position, Target.transform.position) >= _pathRadius)
        {
            transform.rotation = _normalRotation;
        }
        else
        {
            transform.LookAt(Target.transform.position);
        }
        Move();
    }

    private void Move()
    {
        _rb.linearVelocity = _speed * transform.forward;
    }
}
