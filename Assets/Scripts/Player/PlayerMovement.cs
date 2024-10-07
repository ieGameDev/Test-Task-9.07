using UnityEngine.AI;
using UnityEngine;
using Assets.Scripts.Logic;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private PlayerAnimator _animator;

        private NavMeshAgent _player;
        private Transform _spawnPoint;
        private PlayerStateMachine _stateMachine;
        private PlayerStateHandler _stateHandler;

        private float _rotationSpeed = 5f;

        public Dictionary<int, Transform> Waypoints { get; set; }
        public List<int> SortedWaypointIds { get; set; }
        public int CurrentWaypointIndex { get; set; } = 0;

        public void Construct(GameObject spawnPoint, PlayerStateMachine stateMachine, PlayerStateHandler stateHandler)
        {
            _spawnPoint = spawnPoint.transform;
            _stateMachine = stateMachine;
            _stateHandler = stateHandler;
        }

        private void Start()
        {
            _player = GetComponent<NavMeshAgent>();
            _player.speed = _movementSpeed;

            FindWaypoints();
            MoveToNextWaypoint();
        }

        private void Update() => 
            _stateHandler.HandleState();

        public void MoveToNextWaypoint()
        {
            if (SortedWaypointIds.Count == 0)
                return;            

            _player.SetDestination(NextWaypoint().position);

            CurrentWaypointIndex = (CurrentWaypointIndex + 1) % SortedWaypointIds.Count;
        }

        public void RotateTowards(Vector3 target)
        {
            Vector3 direction = (target - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }

        public Transform NextWaypoint()
        {
            int nextWaypointId = SortedWaypointIds[CurrentWaypointIndex];
            Transform nextWaypoint = Waypoints[nextWaypointId];
            
            return nextWaypoint;
        }

        public bool ReachedDestination() =>
            !_player.pathPending && _player.remainingDistance < _player.stoppingDistance;

        private void FindWaypoints()
        {
            WayPoint[] waypointObjects = FindObjectsOfType<WayPoint>();
            Waypoints = new Dictionary<int, Transform>();

            for (int i = 0; i < waypointObjects.Length; i++)
                Waypoints.Add(i, waypointObjects[i].transform);

            SortedWaypointIds = Waypoints
                .OrderBy(wp => Vector3.Distance(_spawnPoint.position, wp.Value.position))
                .Select(wp => wp.Key)
                .ToList();
        }
    }
}
