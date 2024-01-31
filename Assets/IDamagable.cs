public interface IDamagable
{
    int MaxHealth { get; }
    int CurrentHealth { get; }

    void TakeDamage(int damageAmount);
    void Heal(int healAmount);
    void Die();
}