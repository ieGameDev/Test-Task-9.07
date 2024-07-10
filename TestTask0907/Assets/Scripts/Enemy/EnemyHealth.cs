using System;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        private float _currentHealth;
        private float _maxHealth = 100;

        public event Action HealthChanged;

        public float CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }
        //public float MaxHealth
        //{
        //    get => _maxHealth;
        //    set => _maxHealth = value;
        //}

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;

            HealthChanged?.Invoke();
        }
    }
}
