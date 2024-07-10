using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class WayPoint : MonoBehaviour
    {
        public bool HasEnemy { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
                HasEnemy = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy"))
                HasEnemy = false;
        }
    }
}
