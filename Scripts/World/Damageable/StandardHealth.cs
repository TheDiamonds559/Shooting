using System;
using UnityEngine;

public class StandardHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _maxHealth = 100.0f;
    private float _health;

    public Action DeathE { get; set; }
    public Action<float> DamageE { get; set; }
    public Action<float> HealE { get; set; }


    public void Damage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            DeathE?.Invoke();
        }
        else
        {
            DamageE?.Invoke(damage);
        }
    }

    public void Heal(float amount)
    {
        _health += amount;
        if (_health >= _maxHealth)
        {
            _health = _maxHealth;
        }
        HealE?.Invoke(amount);
    }

    public void RemoveDeathEvent(Action deathFunction)
    {
        DeathE -= deathFunction;
    }

    private void Start()
    {
        _health = _maxHealth;
    }
}
