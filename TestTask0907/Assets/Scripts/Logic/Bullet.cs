using Assets.Scripts.Enemy;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Bullet : MonoBehaviour
    {
        private int _damage;

        public void Initialize(int damage) =>
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
