using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerStateHandler
    {
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerAttack _playerAttack;
        private readonly PlayerStateMachine _stateMachine;
        private readonly PlayerAnimator _animator;

        public PlayerStateHandler(PlayerMovement playerMovement, PlayerAttack playerAttack, PlayerStateMachine stateMachine, PlayerAnimator playerAnimator)
        {
            _playerMovement = playerMovement;
            _playerAttack = playerAttack;
            _stateMachine = stateMachine;
            _animator = playerAnimator;
        }

        public void HandleState()
        {
            switch (_stateMachine.CurrentState)
            {
                case PlayerState.Idle:
                    HandleIdleState();
                    _animator.PlayIdle();
                    break;

                case PlayerState.Run:
                    HandleRunState();
                    _animator.PlayRun();
                    break;
            }
        }

        private void HandleIdleState()
        {
            if (!_playerAttack.HasEnemiesOnWaypoint() && _playerMovement.ReachedDestination())
            {
                _stateMachine.SetState(PlayerState.Run);
                _playerMovement.MoveToNextWaypoint();
            }
            else if (_playerAttack.HasEnemiesOnWaypoint())
                _playerMovement.RotateTowards(_playerMovement.NextWaypoint().position);
        }

        private void HandleRunState()
        {
            if (_playerMovement.ReachedDestination())
                _stateMachine.SetState(PlayerState.Idle);
        }
    }
}
