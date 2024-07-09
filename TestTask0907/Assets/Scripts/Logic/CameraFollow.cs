using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _distance;
        [SerializeField] private float _height;
        [SerializeField] private float _smoothTime;

        private Transform _player;
        private Vector3 _currentVelocity;

        private void LateUpdate()
        {
            if (_player == null)
                return;

            Vector3 lookAtPosition = _player.position + Vector3.up * _height;
            Vector3 targetPosition = lookAtPosition - _player.forward * _distance;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothTime);
            transform.LookAt(lookAtPosition);
        }

        public void Follow(GameObject player) =>
            _player = player.transform;
    }
}
