using Assets.Scripts.Enemy;
using Assets.Scripts.ObjectPool;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class Bullet : MonoBehaviour
    {
        private const string LayerName = "Enemy";

        private float _damage;
        private int _enemyLayer;
        private PoolBase<BulletComponent> _bulletPool;
        private BulletComponent _bulletComponent;

        public void Initialize(float damage, PoolBase<BulletComponent> bulletPool, BulletComponent bulletComponent)
        {
            _damage = damage;
            _enemyLayer = LayerMask.NameToLayer(LayerName);
            _bulletPool = bulletPool;
            _bulletComponent = bulletComponent;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _enemyLayer)
            {
                IHealth health = null;

                if (other.TryGetComponent(out health))
                    health.TakeDamage(_damage);

                _bulletPool.Return(_bulletComponent);
            }
        }
    }
}
