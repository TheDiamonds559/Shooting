using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSphere : MonoBehaviour
{
    [SerializeField] private float _damage = 5.0f;
    [SerializeField] private float _hitRate = .75f;
    [SerializeField] private float _radius = .5f;
    [SerializeField] private LayerMask _ignoreMask;

    private List<IHealth> _toHit = new List<IHealth>();
    private bool _isHitting = false;

    private IEnumerator _currentHit;

    public bool CanHit()
    {
        _toHit.Clear();
        Collider[] c = Physics.OverlapSphere(transform.position, _radius);

        if (c.Length <= 0) return false;

        foreach (Collider cc in c)
        {
            if (((1 << cc.gameObject.layer) & _ignoreMask) != 0) continue;
            IHealth h = cc.GetComponent<IHealth>();
            if (h == null) continue;
            _toHit.Add(h);
        }
        if (_toHit.Count > 0)
            return true;
        return false;
    }

    public void Hit()
    {
        if (_isHitting) return;
        _isHitting = true;
        foreach (IHealth h in _toHit)
        {
            h.Damage(_damage);
        }

        if (_currentHit != null)
            StopCoroutine(_currentHit);
        _currentHit = HasHit();
        StartCoroutine(_currentHit);
    }

    public void Initialise()
    {
        if (_currentHit != null)
        {
            StopCoroutine(_currentHit);
            _currentHit = null;
        }
        _isHitting = false;
    }

    private IEnumerator HasHit()
    {
        yield return new WaitForSeconds(_hitRate);
        _isHitting = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
