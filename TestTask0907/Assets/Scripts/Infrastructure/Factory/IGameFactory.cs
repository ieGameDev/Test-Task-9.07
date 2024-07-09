using Assets.Scripts.Infrastructure.DI;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateEnemy(GameObject initialPoint);
        GameObject CreatePlayer(GameObject initialPoint);
    }
}