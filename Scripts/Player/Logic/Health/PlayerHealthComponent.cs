using UnityEngine;

[RequireComponent(typeof(IHealth))]
public class PlayerHealthComponent : PlayerComponent
{
    private IHealth _health;

    public override void InitialiseComponent(PlayerManager playerManager)
    {
        _health = GetComponent<IHealth>();
        base.InitialiseComponent(playerManager);
    }

    protected override void AddEvents()
    {
        _health.DamageE += Damage;
        _health.DeathE += Death;
    }

    protected override void RemoveEvents()
    {
        _health.DamageE -= Damage;
        _health.DeathE -= Death;
    }

    private void Damage(float damage)
    {
        Debug.Log("Damaged");
        //Add logic here for later
    }

    private void Death()
    {
        Debug.Log("Died");
    }
}
