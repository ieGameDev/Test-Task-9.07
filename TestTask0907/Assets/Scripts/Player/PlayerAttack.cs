using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private float _rayLength = 100f;
        [SerializeField] private LayerMask _enemyLayerMask;

        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private float _bulletSpeed = 10f;

        private Camera _camera;
        private float _damage = 25f;

        private void Start() => 
            _camera = Camera.main;

        private void Update()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _rayLength, _enemyLayerMask))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);

                    Vector3 direction = (hit.point - _bulletSpawnPoint.position).normalized;

                    bullet.GetComponent<Rigidbody>().velocity = direction * _bulletSpeed;
                    bullet.GetComponent<Bullet>().Initialize(_damage);

                    Destroy(bullet, 2f);
                }
            }
        }

        public bool HasEnemiesOnWaypoint()
        {
            WayPoint waypoint = _playerMovement.NextWaypoint().GetComponent<WayPoint>();

            if (waypoint != null && waypoint.HasEnemy)
                return true;

            return false;
        }
    }
}
