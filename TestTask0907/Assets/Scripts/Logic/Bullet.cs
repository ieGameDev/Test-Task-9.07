using Assets.Scripts.Enemy;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Bullet : MonoBehaviour
    {
        private const string LayerName = "Enemy";

        private float _damage;
        private int _enemyLayer;

        public void Initialize(float damage)
        {
            _damage = damage;
            _enemyLayer = LayerMask.NameToLayer(LayerName);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _enemyLayer)
            {
                IHealth health = null;

                if (other.TryGetComponent(out health))
                    health.TakeDamage(_damage);

                Destroy(gameObject);
            }
        }
    }
}
