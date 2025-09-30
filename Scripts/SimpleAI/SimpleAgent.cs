using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleAgent : MonoBehaviour
{
    [field: SerializeField] public GameObject Target { get; set; }
    [SerializeField] private float _speed = 5.0f;
    private IAgentMovement _movement;

    private void Start()
    {
        _movement = GetComponent<IAgentMovement>();
    }

    public void Move()
    {
        _movement.MoveTowards(Target);
    }
}
