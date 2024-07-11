using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;

        private void Awake() => 
            EnableAnimator();

        public void Initialize() =>
            _animator = GetComponent<Animator>();

        public void EnableAnimator() =>
            _animator.enabled = true;

        public void DisableAnimator() =>
           _animator.enabled = false;
    }
}
