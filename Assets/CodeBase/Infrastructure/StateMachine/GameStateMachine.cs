﻿using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.Factory.Ui;
using Assets.CodeBase.Infrastructure.Services.StaticData;
using Assets.CodeBase.Infrastructure.Services.Window;
using System;
using System.Collections.Generic;

namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;

        private IExitableState _currentState;

        public GameStateMachine(ICoroutinerRunner coroutinerRunner, SceneLoader sceneLoader, AllServices allServices)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, coroutinerRunner, sceneLoader, allServices),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, allServices.Single<IUiFactory>(), allServices.Single<IWindowService>(), allServices.Single<IStaticDataService>())
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState>(string scene) where TState : class, ILoadLevelState
        {
            TState state = ChangeState<TState>();
            state.Enter(scene);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();

            TState state = GetState<TState>();
            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}