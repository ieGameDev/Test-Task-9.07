using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private EnemyAnimator _enemyAnimator;
        [SerializeField] private RagdollHandler _rigdollHandler;

        public event Action DeathHappened;

        private void Awake()
        {
            _enemyAnimator.Initialize();
            _rigdollHandler.Initialize();

            _enemyHealth.HealthChanged += HealthChanged;
        }

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

            _enemyAnimator.DisableAnimator();
            _rigdollHandler.Enable();

            StartCoroutine(Death());
        }

        private IEnumerator Death()
        {
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }
}
