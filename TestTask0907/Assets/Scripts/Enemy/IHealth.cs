using System;

namespace Assets.Scripts.Enemy
{
    public interface IHealth
    {
        float CurrentHealth { get; set; }
        float MaxHealth { get; }

        event Action HealthChanged;

        void TakeDamage(float damage);
    }
}