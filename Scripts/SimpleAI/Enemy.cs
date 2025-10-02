using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [field:SerializeField] public GameObject Target { get; set; }
    private Rigidbody _rb;

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _pathRadius = 5.0f;
    [SerializeField] private Quaternion _normalRotation;

    [Header("Enemy Attack Variables")]
    [SerializeField] private DamageSphere _enemyAttack;

    private IHealth _health;

    private EnemyState _state;


    private void Start()
    {
        _health = GetComponent<IHealth>();
        _health.DeathE += Death;
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _state = EnemyState.Walking;
        _enemyAttack.Initialise();
    }

    private void Update()
    {
        if (Target == null) return;
        
        if (_enemyAttack.CanHit())
        {
            _enemyAttack.Hit();
            return;
        }

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

    private void Death()
    {
        ObjectPoolManager.Instance.Uninstantiate("enemy", gameObject);
    }

    private void Move()
    {
        _rb.linearVelocity = _speed * transform.forward;
    }

    private void OnDestroy()
    {
        _health.DeathE -= Death;
    }
}

public enum EnemyState
{
    Walking,
    Attacking
}
