using Assets.Scripts.Enemy;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Bullet : MonoBehaviour
    {
        private float _damage;

        public void Initialize(float damage) =>
            _damage = damage;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyHealth>().TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
