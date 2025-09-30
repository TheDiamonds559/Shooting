using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _velocity = 50.0f;
    [SerializeField] private TrailRenderer _trailRenderer;

    private float _life = 3.0f;
    private IEnumerator _lifetime;
    private float _damage;

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, .5f, transform.forward, out hit, 1))
        {
            StopCoroutine(_lifetime);
            _lifetime = null;
            if (hit.collider.GetComponent<IHealth>() != null)
            {
                hit.collider.GetComponent<IHealth>().Damage(_damage);
            }
            GameObject g = ObjectPoolManager.Instance.GetPrefab("spark");
            g.transform.position = hit.point;
            g.GetComponent<HitEffect>().PlayEffect();
            ObjectPoolManager.Instance.Uninstantiate("bullet", gameObject);
        }
    }

    public void Spawn(Transform startPos, Transform forward, float damage)
    {
        _damage = damage;
        _rb.linearVelocity = Vector3.zero;
        transform.position = startPos.position;
        transform.forward = forward.forward;
        _trailRenderer.enabled = true;
        _lifetime = BulletLife();
        StartCoroutine(BulletLife());
    }

    public void Move()
    {
        _rb.AddForce(_velocity * transform.forward);
    }

    private IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(_life);
        ObjectPoolManager.Instance.Uninstantiate("bullet", gameObject);
    }

    private void OnEnable()
    {
        _trailRenderer.Clear();
        _trailRenderer.enabled = false;

    }
}
