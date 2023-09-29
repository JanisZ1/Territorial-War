using Assets.CodeBase.Infrastructure.Services.Calculations;
using Assets.CodeBase.Infrastructure.Services.Factory;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : ILoadLevelState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGreenCommandSpawner _greenCommandSpawner;
        private readonly IWarriorFactory _warriorFactory;
        private readonly IClosestEnemyCalculator _closestEnemyCalculator;
        private IArcherFactory _archerFactory;
        private const string GreenBaseTag = "GreenBase";
        private const string RedBaseTag = "RedBase";

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGreenCommandSpawner greenCommandSpawner, IWarriorFactory warriorFactory, IArcherFactory archerFactory, IClosestEnemyCalculator closestEnemyCalculator)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _greenCommandSpawner = greenCommandSpawner;
            _warriorFactory = warriorFactory;
            _archerFactory = archerFactory;
            _closestEnemyCalculator = closestEnemyCalculator;
        }

        public void Enter(string scene) =>
            _sceneLoader.Load(scene, OnLoaded);

        private void OnLoaded()
        {
            GameObject.FindObjectOfType<QueueChecker>().Construct(_greenCommandSpawner);
            InitializeGreenBase();
            InitializeRedBase();
        }

        private void InitializeGreenBase()
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(GreenBaseTag);
            gameObject.GetComponentInChildren<QueueWarrior>().Construct(_warriorFactory, _greenCommandSpawner);
            gameObject.GetComponentInChildren<QueueArcher>().Construct(_archerFactory, _greenCommandSpawner);
        }

        private void InitializeRedBase()
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(RedBaseTag);
            gameObject.GetComponentInChildren<RedBaseQueueWarrior>().Construct(_warriorFactory);
        }

        public void Exit()
        {
        }
    }
}