using Assets.CodeBase.Infrastructure.Services.AiUnitControll;
using Assets.CodeBase.Infrastructure.Services.Factory.Spawners;
using Assets.CodeBase.Infrastructure.Services.Factory.Ui;
using Assets.CodeBase.Infrastructure.Services.Factory.Unit;
using Assets.CodeBase.Infrastructure.Services.GreenCommandUnitsHandler;
using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.Infrastructure.Services.StaticData;
using Assets.CodeBase.Logic.Spawners;
using Assets.CodeBase.Logic.Ui;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : ILoadLevelState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISpawnersFactory _spawnersFactory;
        private readonly SceneLoader _sceneLoader;
        private readonly IAiUnitSpawnControll _aiUnitSpawnControll;
        private readonly IChooseCommandMediator _chooseCommandMediator;
        private readonly IUiFactory _uiFactory;
        private readonly IGreenCommandUnitsHandler _greenCommandUnitsHandler;
        private readonly IRedCommandUnitsHandler _redCommandUnitsHandler;
        private readonly IStaticDataService _staticDataService;
        private readonly IUnitFactory _greenCommandUnitFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, ISpawnersFactory spawnersFactory, SceneLoader sceneLoader, IAiUnitSpawnControll aiUnitSpawnControll, IChooseCommandMediator chooseCommandMediator, IUiFactory uiFactory, IGreenCommandUnitsHandler greenCommandUnitsHandler, IRedCommandUnitsHandler redCommandUnitsHandler, IStaticDataService staticDataService, IUnitFactory greenCommandUnitFactory)
        {
            _gameStateMachine = gameStateMachine;
            _spawnersFactory = spawnersFactory;
            _sceneLoader = sceneLoader;
            _aiUnitSpawnControll = aiUnitSpawnControll;
            _chooseCommandMediator = chooseCommandMediator;
            _uiFactory = uiFactory;
            _greenCommandUnitsHandler = greenCommandUnitsHandler;
            _redCommandUnitsHandler = redCommandUnitsHandler;
            _staticDataService = staticDataService;
            _greenCommandUnitFactory = greenCommandUnitFactory;
        }

        public void Enter(string scene) =>
            _sceneLoader.Load(scene, OnLoaded);

        private void OnLoaded()
        {
            _staticDataService.Load();
            _uiFactory.CreateUiRoot();
            CreateChooseCommandButtons();

            GreenCommandUnitSpawner greenCommandUnitSpawner = InitializeGreenUnitSpawner();
            RedCommandUnitSpawner redCommandUnitSpawner = InitializeRedUnitSpawner();

            _aiUnitSpawnControll.InitSpawners(greenCommandUnitSpawner, redCommandUnitSpawner);
        }

        private void CreateChooseCommandButtons()
        {
            GameObject chooseCommandButtons = _uiFactory.CreateChooseCommandButtons();

            foreach (ChooseCommandButton chooseButton in chooseCommandButtons.GetComponentsInChildren<ChooseCommandButton>())
                chooseButton.Construct(_chooseCommandMediator);
        }

        private GreenCommandUnitSpawner InitializeGreenUnitSpawner()
        {
            GameObject gameObject = _spawnersFactory.CreateCommandSpawner(CommandColor.Green);
            GreenCommandUnitSpawner greenCommandUnitSpawner = gameObject.GetComponentInChildren<GreenCommandUnitSpawner>();
            greenCommandUnitSpawner.Construct(_greenCommandUnitFactory, _redCommandUnitsHandler, _greenCommandUnitsHandler);

            return greenCommandUnitSpawner;
        }

        private RedCommandUnitSpawner InitializeRedUnitSpawner()
        {
            GameObject gameObject = _spawnersFactory.CreateCommandSpawner(CommandColor.Red);
            RedCommandUnitSpawner redCommandUnitSpawner = gameObject.GetComponentInChildren<RedCommandUnitSpawner>();
            redCommandUnitSpawner.Construct(_greenCommandUnitFactory, _redCommandUnitsHandler, _greenCommandUnitsHandler);

            return redCommandUnitSpawner;
        }

        public void Exit()
        {
        }
    }
}