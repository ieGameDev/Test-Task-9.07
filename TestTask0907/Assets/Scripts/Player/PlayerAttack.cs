using Assets.Scripts.InputServices;
using Assets.Scripts.Logic;
using Assets.Scripts.ObjectPool;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private const int PreloadCount = 20;

        [Header("Shooting Settings")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private float _rayLength = 100f;
        [SerializeField] private LayerMask _enemyLayerMask;

        [Header("Bullet Settings")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;

        private IInputService _input;
        private PoolBase<GameObject> _bulletPool;
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
            _bulletPool = new PoolBase<GameObject>(PreloadBullet, GetAction, ReturnAction, PreloadCount);

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

            GameObject bullet = BulletInitialize(targetPoint);
            StartCoroutine(ReturnBullet(bullet));
        }

        private GameObject BulletInitialize(Vector3 targetPoint)
        {
            GameObject bullet = _bulletPool.Get();

            Vector3 direction = (targetPoint - _bulletSpawnPoint.position).normalized;

            bullet.GetComponent<Rigidbody>().velocity = direction * _bulletSpeed;
            bullet.GetComponent<Bullet>().Initialize(_damage, _bulletPool);
            return bullet;
        }

        private IEnumerator ReturnBullet(GameObject bullet)
        {
            yield return new WaitForSeconds(2);
            _bulletPool.Return(bullet);
        }

        private GameObject PreloadBullet() =>
            Instantiate(_bulletPrefab);

        private void GetAction(GameObject bullet)
        {
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = _bulletSpawnPoint.rotation;

            TrailComponent(bullet)?.Clear();
            TrailComponent(bullet).enabled = true;

            bullet.SetActive(true);
        }

        private void ReturnAction(GameObject bullet)
        {
            TrailComponent(bullet).enabled = false;
            bullet.SetActive(false);
        }

        private static TrailRenderer TrailComponent(GameObject bullet) => 
            bullet.GetComponent<TrailRenderer>();
    }
}
