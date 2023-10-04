using Assets.CodeBase.Infrastructure.Services.Factory;
using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.Infrastructure.Services.StaticData;
using Assets.CodeBase.Logic.Spawners;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : ILoadLevelState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IRedCommandUnitsHandler _redCommandUnitsHandler;
        private readonly IStaticDataService _staticDataService;
        private readonly IUnitFactory _greenCommandUnitFactory;
        private const string GreenBaseTag = "GreenBase";
        private const string RedBaseTag = "RedBase";

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IRedCommandUnitsHandler redCommandUnitsHandler, IStaticDataService staticDataService, IUnitFactory greenCommandUnitFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _redCommandUnitsHandler = redCommandUnitsHandler;
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
            gameObject.GetComponentInChildren<GreenCommandUnitSpawner>().Construct(_greenCommandUnitFactory, _redCommandUnitsHandler);
        }

        private void InitializeRedBase()
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(RedBaseTag);
            gameObject.GetComponentInChildren<RedCommandUnitSpawner>().Construct(_greenCommandUnitFactory,_redCommandUnitsHandler);
        }

        public void Exit()
        {
        }
    }
}