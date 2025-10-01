using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _velocity = 50.0f;
    [SerializeField] private TrailRenderer _trailRenderer;

    private float _life = 3.0f;
    private IEnumerator _lifetime;
    private float _damage;

    private void FixedUpdate()
    {
        CalculateBulletVelocity();
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, .5f, v.normalized, out hit, v.magnitude * Time.deltaTime))
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
        MoveBullet();
    }

    public void Spawn(Transform startPos, Transform forward, float damage)
    {
        _damage = damage;
        transform.position = startPos.position;
        transform.forward = forward.forward;
        _trailRenderer.enabled = true;
        _lifetime = BulletLife();
        StartCoroutine(_lifetime);
    }

    public void Move()
    {
        u = transform.forward * _velocity;
    }

    private Vector3 v, u, s;
    private Vector3 a = Physics.gravity;
    private void CalculateBulletVelocity()
    {
        float t2 = Time.deltaTime * Time.deltaTime;

        v = u + a * Time.deltaTime;

        s = u * Time.deltaTime + 0.5f * a * t2;

        u = v;
    }

    private void MoveBullet()
    {
        transform.position += s;
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
