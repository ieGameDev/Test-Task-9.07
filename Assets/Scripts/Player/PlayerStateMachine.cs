namespace Assets.Scripts.Player
{
    public enum PlayerState
    {
        Idle,
        Run
    }

    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }

        public PlayerStateMachine() => 
            CurrentState = PlayerState.Idle;

        public void SetState(PlayerState state) => 
            CurrentState = state;
    }
}
