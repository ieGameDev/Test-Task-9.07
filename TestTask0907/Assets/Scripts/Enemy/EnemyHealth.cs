using System;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        private float _currentHealth;
        private float _maxHealth = 100f;

        public event Action HealthChanged;

        public float CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        public float MaxHealth => 
            _maxHealth;

        private void Start() =>
            CurrentHealth = MaxHealth;

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;

            HealthChanged?.Invoke();
        }
    }
}
