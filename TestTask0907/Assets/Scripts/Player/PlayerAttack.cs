using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;

        public bool HasEnemiesOnWaypoint()
        {
            WayPoint waypoint = _playerMovement.NextWaypoint().GetComponent<WayPoint>();

            if (waypoint != null && waypoint.HasEnemy)
                return true;

            return false;
        }
    }
}
