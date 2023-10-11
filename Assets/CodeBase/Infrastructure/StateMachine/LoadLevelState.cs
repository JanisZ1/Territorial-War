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
        private readonly SceneLoader _sceneLoader;
        private readonly IChooseCommandMediator _chooseCommandMediator;
        private readonly IUiFactory _uiFactory;
        private readonly IGreenCommandUnitsHandler _greenCommandUnitsHandler;
        private readonly IRedCommandUnitsHandler _redCommandUnitsHandler;
        private readonly IStaticDataService _staticDataService;
        private readonly IUnitFactory _greenCommandUnitFactory;
        private const string GreenBaseTag = "GreenBase";
        private const string RedBaseTag = "RedBase";

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IChooseCommandMediator chooseCommandMediator, IUiFactory uiFactory, IGreenCommandUnitsHandler greenCommandUnitsHandler, IRedCommandUnitsHandler redCommandUnitsHandler, IStaticDataService staticDataService, IUnitFactory greenCommandUnitFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
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

            InitializeGreenBase();
            InitializeRedBase();
        }

        private void CreateChooseCommandButtons()
        {
            GameObject chooseCommandButtons = _uiFactory.CreateChooseCommandButtons();

            foreach (ChooseCommandButton chooseButton in chooseCommandButtons.GetComponentsInChildren<ChooseCommandButton>())
                chooseButton.Construct(_chooseCommandMediator);
        }

        private void InitializeGreenBase()
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(GreenBaseTag);
            gameObject.GetComponentInChildren<GreenCommandUnitSpawner>().Construct(_greenCommandUnitFactory, _redCommandUnitsHandler, _greenCommandUnitsHandler);
        }

        private void InitializeRedBase()
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(RedBaseTag);
            gameObject.GetComponentInChildren<RedCommandUnitSpawner>().Construct(_greenCommandUnitFactory, _redCommandUnitsHandler, _greenCommandUnitsHandler);
        }

        public void Exit()
        {
        }
    }
}