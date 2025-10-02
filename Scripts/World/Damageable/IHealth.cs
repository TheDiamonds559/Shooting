using System;

public interface IHealth
{
    public void Damage(float damage);
    public void Heal(float amount);

    public float GetMaxHealth();
    public float GetHealth();

    public Action DeathE { get; set; }
    public Action<float> DamageE { get; set; }
    public Action<float> HealE { get; set; }
}