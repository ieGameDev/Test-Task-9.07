using Assets.Scripts.Enemy;
using Assets.Scripts.Infrastructure.AssetsManager;
using Assets.Scripts.Infrastructure.DI;
using Assets.Scripts.InputServices;
using Assets.Scripts.Player;
using Assets.Scripts.StaticData;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string PlayerPath = "Player/Player";
        private const string EnemyPath = "Enemy/Enemy";
        private const string PlayerDataPath = "StaticData/PlayerData";

        private readonly IAssetsProvider _assetProvider;

        private GameObject _player;

        public GameFactory(IAssetsProvider assetProvider) =>
            _assetProvider = assetProvider;

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            _player = _assetProvider.Instantiate(PlayerPath, initialPoint.transform.position + Vector3.up * 1f);

            PlayerMovement playerMovement = _player.GetComponent<PlayerMovement>();
            PlayerAnimator playerAnimator = _player.GetComponent<PlayerAnimator>();
            PlayerAttack playerAttack = _player.GetComponent<PlayerAttack>();

            IInputService input = DIContainer.Container.Single<IInputService>();
            PlayerStaticData staticData = Resources.Load<PlayerStaticData>(PlayerDataPath);

            float damage = staticData.Damage; 
            float bulletSpeed = staticData.BulletSpeed;
            playerAttack.Construct(input, damage, bulletSpeed);

            PlayerStateMachine stateMachine = new PlayerStateMachine();
            PlayerStateHandler playerStateHandler = new PlayerStateHandler(playerMovement, playerAttack, stateMachine, playerAnimator);

            playerMovement.Construct(initialPoint, stateMachine, playerStateHandler);

            return _player;
        }

        public GameObject CreateEnemy(GameObject initialPoint)
        {
            GameObject enemy = _assetProvider.Instantiate(EnemyPath, initialPoint.transform.position + Vector3.up * 1f);
            enemy.GetComponent<EnemyMove>().Construct(_player);

            return enemy;
        }
    }
}
