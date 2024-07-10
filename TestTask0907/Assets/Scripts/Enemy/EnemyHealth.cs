using System;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        private int _currentHealth;
        private int _maxHealth = 100;

        public event Action HealthChanged;

        public int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        private void Start() => 
            CurrentHealth = _maxHealth;

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;

            HealthChanged?.Invoke();
        }
    }
}
