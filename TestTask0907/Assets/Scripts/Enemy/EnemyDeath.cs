using System;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;

        public event Action DeathHappened;

        private void Start() =>
            _enemyHealth.HealthChanged += HealthChanged;

        private void OnDestroy() =>
            _enemyHealth.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (_enemyHealth.CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            _enemyHealth.HealthChanged -= HealthChanged;
            DeathHappened?.Invoke();

            Destroy(gameObject);
        }
    }
}
