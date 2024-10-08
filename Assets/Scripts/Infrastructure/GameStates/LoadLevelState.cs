﻿using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.GameStates
{
    public class LoadLevelState : IState
    {
        private const string PlayerInitialPointTag = "PlayerInitialPoint";
        private const string EnemyInitialPointTag = "EnemyInitialPoint";

        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            OnLoaded();
            _gameStateMachine.Enter<GameLoopState>();

            Debug.Log("LoadLevelState");
        }

        private void OnLoaded()
        {
            GameObject player = InitialPlayer();
            CameraFollow(player);

            InitialEnemies();
        }

        private GameObject InitialPlayer() =>
            _gameFactory.CreatePlayer(GameObject.FindWithTag(PlayerInitialPointTag));

        private void CameraFollow(GameObject player) => 
            Camera.main.GetComponent<CameraFollow>().Follow(player);

        private void InitialEnemies()
        {
            GameObject[] enemyPoints = GameObject.FindGameObjectsWithTag(EnemyInitialPointTag);

            foreach (GameObject enemyPoint in enemyPoints)
                _gameFactory.CreateEnemy(enemyPoint);
        }
    }
}