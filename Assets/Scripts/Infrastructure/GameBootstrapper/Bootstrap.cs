﻿using Assets.Scripts.Infrastructure.DI;
using Assets.Scripts.Infrastructure.GameStates;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.GameBootstrapper
{
    public class Bootstrap : MonoBehaviour
    {
        public GameStateMachine StateMachine;

        private void Awake()
        {
            StateMachine = new GameStateMachine(DIContainer.Container);
            StateMachine.Enter<BootstrapState>();

            Debug.Log("BootstrapState");

            DontDestroyOnLoad(this);
        }
    }
}
