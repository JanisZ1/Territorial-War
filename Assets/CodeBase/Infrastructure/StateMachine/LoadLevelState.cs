using Assets.CodeBase.Infrastructure.Services.Calculations;
using Assets.CodeBase.Infrastructure.Services.Factory;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : ILoadLevelState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IWarriorFactory _warriorFactory;
        private readonly IClosestEnemyCalculator _closestEnemyCalculator;
        private IArcherFactory _archerFactory;
        private const string GreenBaseTag = "GreenBase";
        private const string RedBaseTag = "RedBase";

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IWarriorFactory warriorFactory, IArcherFactory archerFactory, IClosestEnemyCalculator closestEnemyCalculator)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _warriorFactory = warriorFactory;
            _archerFactory = archerFactory;
            _closestEnemyCalculator = closestEnemyCalculator;
        }

        public void Enter(string scene) =>
            _sceneLoader.Load(scene, OnLoaded);

        private void OnLoaded()
        {
            InitializeGreenBase();
            InitializeRedBase();
        }

        private void InitializeGreenBase()
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(GreenBaseTag);
            gameObject.GetComponentInChildren<QueueWarrior>().Construct(_warriorFactory);
            gameObject.GetComponentInChildren<QueueArcher>().Construct(_archerFactory);
        }

        private void InitializeRedBase()
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(RedBaseTag);
            gameObject.GetComponentInChildren<QueueWarrior>().Construct(_warriorFactory);
        }

        public void Exit()
        {
        }
    }
}