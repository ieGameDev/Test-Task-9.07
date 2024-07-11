using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class WayPoint : MonoBehaviour
    {
        private const string EnemyTag = "Enemy";

        private List<Collider> _enemiesOnWaypoint = new List<Collider>();

        public bool HasEnemy => _enemiesOnWaypoint.Count > 0;

        private void Update() =>
            _enemiesOnWaypoint.RemoveAll(item => item == null);

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(EnemyTag))
                _enemiesOnWaypoint.Add(other);
        }
    }
}
