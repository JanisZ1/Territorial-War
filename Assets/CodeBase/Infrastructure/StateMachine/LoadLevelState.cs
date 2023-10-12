using Assets.CodeBase.Infrastructure.Services.Factory.Ui;
using Assets.CodeBase.Infrastructure.Services.StaticData;
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
        private readonly IStaticDataService _staticDataService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IChooseCommandMediator chooseCommandMediator, IUiFactory uiFactory, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _chooseCommandMediator = chooseCommandMediator;
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
        }

        public void Enter(string scene) =>
            _sceneLoader.Load(scene, OnLoaded);

        private void OnLoaded()
        {
            _staticDataService.Load();
            _uiFactory.CreateUiRoot();
            CreateChooseCommandButtons();
        }

        private void CreateChooseCommandButtons()
        {
            GameObject chooseCommandButtons = _uiFactory.CreateChooseCommandButtons();

            foreach (ChooseCommandButton chooseButton in chooseCommandButtons.GetComponentsInChildren<ChooseCommandButton>())
                chooseButton.Construct(_chooseCommandMediator);
        }

        public void Exit()
        {
        }
    }
}