using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class RagdollHandler : MonoBehaviour
    {
        private List<Rigidbody> _rigidbodies;

        public void Initialize()
        {
            _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
            _rigidbodies.RemoveAll(x => x.gameObject == gameObject);
            Disable();
        }

        public void Enable()
        {
            foreach (var rigidbody in _rigidbodies)
                rigidbody.isKinematic = false;
        }

        public void Disable()
        {
            foreach (var rigidbody in _rigidbodies)
                rigidbody.isKinematic = true;
        }
    }
}
