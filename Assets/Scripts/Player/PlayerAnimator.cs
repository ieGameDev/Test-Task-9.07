using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private static readonly int Run = Animator.StringToHash("Run");

        public void PlayRun() =>
            _animator.SetBool(Run, true);

        public void PlayIdle() =>
            _animator.SetBool(Run, false);
    }
}
