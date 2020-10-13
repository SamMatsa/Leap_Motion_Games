using System;

public class HealthSystem 
{
    public event EventHandler OnHealthChanged;
    private int health;
    private int HEALTH_MAX;

    public HealthSystem(int healthMax)
    {
        HEALTH_MAX = healthMax;
        this.health = healthMax;
    }

    public int GetHealt()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return (float)health / HEALTH_MAX;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0) health = 0;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > HEALTH_MAX) health = HEALTH_MAX;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
}
