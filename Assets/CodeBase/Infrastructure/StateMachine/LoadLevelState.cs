using Assets.CodeBase.Infrastructure.Services.Factory.Ui;
using Assets.CodeBase.Infrastructure.Services.StaticData;
using Assets.CodeBase.Infrastructure.Services.Window;
using Assets.CodeBase.StaticData;

namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : ILoadLevelState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IUiFactory _uiFactory;
        private readonly IWindowService _windowService;
        private readonly IStaticDataService _staticDataService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IUiFactory uiFactory, IWindowService windowService, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _windowService = windowService;
            _staticDataService = staticDataService;
        }

        public void Enter(string scene) =>
            _sceneLoader.Load(scene, OnLoaded);

        private void OnLoaded()
        {
            _staticDataService.Load();
            _uiFactory.CreateUiRoot();
            OpenChooseCommandWindow();
        }

        private void OpenChooseCommandWindow() => 
            _windowService.OpenWindow(WindowType.ChooseCommand);

        public void Exit()
        {
        }
    }
}