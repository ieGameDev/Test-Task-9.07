using Assets.Scripts.Enemy;
using Assets.Scripts.Infrastructure.AssetsManager;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string PlayerPath = "Player/Player";
        private const string EnemyPath = "Enemy/Enemy";
        private readonly IAssetsProvider _assetProvider;

        public GameFactory(IAssetsProvider assetProvider) => 
            _assetProvider = assetProvider;

        public GameObject PlayerGameObject;

        public GameObject CreatePlayer(GameObject initialPoint)
        {
            PlayerGameObject = _assetProvider.Instantiate(PlayerPath, initialPoint.transform.position + Vector3.up * 1f);

            return PlayerGameObject;
        }

        public GameObject CreateEnemy(GameObject initialPoint)
        {
            GameObject enemy = _assetProvider.Instantiate(EnemyPath, initialPoint.transform.position + Vector3.up * 1f);
            enemy.GetComponent<EnemyMove>().Construct(PlayerGameObject);

            return enemy;
        }
    }
}
