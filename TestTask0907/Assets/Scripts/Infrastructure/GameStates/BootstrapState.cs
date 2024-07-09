using Assets.Scripts.Infrastructure.AssetsManager;
using Assets.Scripts.Infrastructure.DI;
using Assets.Scripts.Infrastructure.Factory;

namespace Assets.Scripts.Infrastructure.GameStates
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly DIContainer _container;

        public BootstrapState(GameStateMachine gameStateMachine, DIContainer container)
        {
            _gameStateMachine = gameStateMachine;
            _container = container;

            RegisterServices();
        }

        public void Enter() => 
            _gameStateMachine.Enter<LoadLevelState>();

        private void RegisterServices()
        {
            _container.RegisterSingle<IAssetsProvider>(new AssetsProvider());
            _container.RegisterSingle<IGameFactory>(new GameFactory(_container.Single<IAssetsProvider>()));
        }
    }
}