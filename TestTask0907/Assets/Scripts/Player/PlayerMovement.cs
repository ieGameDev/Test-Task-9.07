using UnityEngine.AI;
using UnityEngine;
using Assets.Scripts.Logic;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 3.5f;
        [SerializeField] private PlayerAnimator _animator;

        private Transform _spawnPoint;
        private NavMeshAgent _player;
        private PlayerStateMachine _stateMachine;

        private Dictionary<int, Transform> _waypoints;
        private List<int> _sortedWaypointIds;

        private int _currentWaypointIndex = 0;
        private bool _waitingForTap = false;

        public void Construct(GameObject spawnPoint, PlayerStateMachine stateMachine)
        {
            _spawnPoint = spawnPoint.transform;
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            _player = GetComponent<NavMeshAgent>();
            _player.speed = _movementSpeed;

            FindWaypoints();
            MoveToNextWaypoint();
        }

        private void Update()
        {
            switch (_stateMachine.CurrentState)
            {
                case PlayerState.Idle:
                    HandleIdleState();
                    _animator.PlayIdle();
                    break;
                case PlayerState.Run:
                    HandleRunState();
                    _animator.PlayRun();
                    break;
            }
        }

        private void HandleIdleState()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _stateMachine.SetState(PlayerState.Run);
                MoveToNextWaypoint();
            }
        }

        private void HandleRunState()
        {
            if (ReachedDestination())
                _stateMachine.SetState(PlayerState.Idle);
        }

        private void MoveToNextWaypoint()
        {
            if (_sortedWaypointIds.Count == 0)
                return;

            int nextWaypointId = _sortedWaypointIds[_currentWaypointIndex];
            _player.SetDestination(_waypoints[nextWaypointId].position);

            _currentWaypointIndex = (_currentWaypointIndex + 1) % _sortedWaypointIds.Count;
        }

        private void FindWaypoints()
        {
            WayPoint[] waypointObjects = FindObjectsOfType<WayPoint>();
            _waypoints = new Dictionary<int, Transform>();

            for (int i = 0; i < waypointObjects.Length; i++)
                _waypoints.Add(i, waypointObjects[i].transform);

            _sortedWaypointIds = _waypoints
                .OrderBy(wp => Vector3.Distance(_spawnPoint.position, wp.Value.position))
                .Select(wp => wp.Key)
                .ToList();
        }

        private bool ReachedDestination() =>
            !_player.pathPending && _player.remainingDistance <= _player.stoppingDistance;
    }
}
