using UnityEngine;

[RequireComponent(typeof(IHealth))]
public class PlayerHealthComponent : PlayerComponent
{
    private IHealth Health;

    public override void InitialiseComponent(PlayerManager playerManager)
    {
        Health = GetComponent<IHealth>();
        base.InitialiseComponent(playerManager);
    }

    protected override void AddEvents()
    {
        Health.DamageE += Damage;
        Health.DeathE += Death;
    }

    protected override void RemoveEvents()
    {
        Health.DamageE -= Damage;
        Health.DeathE -= Death;
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
