using Assets.Scripts.InputServices;
using Assets.Scripts.Logic;
using Assets.Scripts.ObjectPool;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private const int PreloadCount = 30;

        [Header("Shooting Settings")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private float _rayLength = 100f;
        [SerializeField] private LayerMask _enemyLayerMask;

        [Header("Bullet Settings")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;

        private IInputService _input;
        private PoolBase<BulletComponent> _bulletPool;
        private Camera _camera;

        private float _damage;
        private float _bulletSpeed;

        public void Construct(IInputService input, float damage, float bulletSpeed)
        {
            _camera = Camera.main;
            _input = input;
            _damage = damage;
            _bulletSpeed = bulletSpeed;
        }

        private void Awake() =>
            _bulletPool = new PoolBase<BulletComponent>(PreloadBullet, GetAction, ReturnAction, PreloadCount);

        public bool HasEnemiesOnWaypoint()
        {
            WayPoint waypoint = _playerMovement.NextWaypoint().GetComponent<WayPoint>();

            if (waypoint != null && waypoint.HasEnemy)
                return true;

            return false;
        }

        public void Shoot()
        {
            if (_input.Tap)
                HandleShoot(_input.TapPlace);
        }

        private void HandleShoot(Vector3 inputPosition)
        {
            Ray ray = _camera.ScreenPointToRay(inputPosition);
            RaycastHit hit;

            Vector3 targetPoint;

            if (Physics.Raycast(ray, out hit, _rayLength, _enemyLayerMask))
                targetPoint = hit.point;
            else
                targetPoint = ray.GetPoint(_rayLength);

            BulletComponent bullet = BulletInitialize(targetPoint);
            StartCoroutine(ReturnBullet(bullet));
        }

        private BulletComponent BulletInitialize(Vector3 targetPoint)
        {
            BulletComponent bullet = _bulletPool.Get();

            Vector3 direction = (targetPoint - _bulletSpawnPoint.position).normalized;

            bullet.Rigidbody.velocity = direction * _bulletSpeed;
            bullet.Bullet.Initialize(_damage, _bulletPool, bullet);
            return bullet;
        }

        private IEnumerator ReturnBullet(BulletComponent bullet)
        {
            yield return new WaitForSeconds(2);
            _bulletPool.Return(bullet);
        }

        private BulletComponent PreloadBullet()
        {
            GameObject bulletObject = Instantiate(_bulletPrefab);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            Rigidbody rigidBody = bulletObject.GetComponent<Rigidbody>();
            TrailRenderer trailRenderer = bulletObject.GetComponent<TrailRenderer>();

            return new BulletComponent(bulletObject, bullet, rigidBody, trailRenderer);
        }

        private void GetAction(BulletComponent bulletComponent)
        {
            GameObject bullet = bulletComponent.BulletObject;
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = _bulletSpawnPoint.rotation;

            bulletComponent.TrailRenderer?.Clear();
            bulletComponent.TrailRenderer.enabled = true;

            bullet.SetActive(true);
        }

        private void ReturnAction(BulletComponent bulletComponent)
        {
            GameObject bullet = bulletComponent.BulletObject;
            bulletComponent.TrailRenderer.enabled = false;

            bullet.SetActive(false);
        }
    }
}
