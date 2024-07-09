using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        private Transform _player;

        public void Construct(GameObject player) => 
            _player = player.transform;

        private void Update()
        {
            if (_player != null)
                LookAtPlayer();
        }

        private void LookAtPlayer()
        {
            Vector3 direction = _player.position - transform.position;
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3.0f * Time.deltaTime);
        }
    }
}