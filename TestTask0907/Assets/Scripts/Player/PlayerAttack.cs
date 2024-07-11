using Assets.Scripts.Infrastructure.DI;
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
        [SerializeField] private float _bulletSpeed = 10f;

        private IInputService _input;
        private PoolBase<GameObject> _bulletPool;
        private Camera _camera;
        private float _damage = 25f;

        private void Awake()
        {
            _input = DIContainer.Container.Single<IInputService>();
            _camera = Camera.main;

            _bulletPool = new PoolBase<GameObject>(PreloadBullet, GetAction, ReturnAction, PreloadCount);
        }

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

            if (Physics.Raycast(ray, out hit, _rayLength, _enemyLayerMask))
            {
                GameObject bullet = _bulletPool.Get();

                Vector3 direction = (hit.point - _bulletSpawnPoint.position).normalized;

                bullet.GetComponent<Rigidbody>().velocity = direction * _bulletSpeed;
                bullet.GetComponent<Bullet>().Initialize(_damage, _bulletPool);

                StartCoroutine(ReturnBullet(bullet));
            }
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
            bullet.SetActive(true);
        }

        private void ReturnAction(GameObject bullet) => 
            bullet.SetActive(false);
    }
}
