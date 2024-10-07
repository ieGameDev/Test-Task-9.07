using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class CloudsRotation : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;

        private void Update()
        {
            float rotationAmount = _rotationSpeed * Time.deltaTime;

            Vector3 currentRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y + rotationAmount, currentRotation.z);
        }
    }
}
