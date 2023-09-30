using Assets.CodeBase.Infrastructure.Services.Factory;
using Assets.CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : ILoadLevelState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly IGreenCommandUnitFactory _greenCommandUnitFactory;
        private const string GreenBaseTag = "GreenBase";
        private const string RedBaseTag = "RedBase";

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IStaticDataService staticDataService, IGreenCommandUnitFactory greenCommandUnitFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _greenCommandUnitFactory = greenCommandUnitFactory;
        }

        public void Enter(string scene) =>
            _sceneLoader.Load(scene, OnLoaded);

        private void OnLoaded()
        {
            _staticDataService.Load();

            InitializeGreenBase();
            InitializeRedBase();
        }

        private void InitializeGreenBase()
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(GreenBaseTag);
            gameObject.GetComponentInChildren<GreenCommandUnitSpawner>().Construct(_greenCommandUnitFactory);
        }

        private void InitializeRedBase()
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(RedBaseTag);
            gameObject.GetComponentInChildren<RedCommandUnitSpawner>().Construct(_greenCommandUnitFactory);
        }

        public void Exit()
        {
        }
    }
}