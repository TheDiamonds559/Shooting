using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class HitEffect : MonoBehaviour
{
    [SerializeField] private string _prefabName;
    private ParticleSystem _particleSystem;

    private bool _hasStarted = false;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void PlayEffect()
    {
        if (_particleSystem == null) _particleSystem = GetComponent<ParticleSystem>();  
        _hasStarted = true;
        _particleSystem.Play();
    }

    private void Update()
    {
        if (!_hasStarted) return;
        if (_particleSystem.isStopped)
            EffectStopped();
    }

    private void EffectStopped()
    {
        _hasStarted = false;
        ObjectPoolManager.Instance.Uninstantiate(_prefabName, gameObject);
    }
}
