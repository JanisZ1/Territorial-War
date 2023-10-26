using Assets.CodeBase.Infrastructure.Services.Factory.Ui;
using Assets.CodeBase.Infrastructure.Services.StaticData;
using Assets.CodeBase.Infrastructure.Services.Window;
using Assets.CodeBase.Logic.GlobalMap;
using Assets.CodeBase.StaticData;
using UnityEngine.SceneManagement;

namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : ILoadLevelState
    {
        private const string GlobalMap = "GlobalMap";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IUiFactory _uiFactory;
        private readonly IScanningLineFactory _scanningLineFactory;
        private readonly IParabolaFactory _parabolaFactory;
        private readonly IWindowService _windowService;
        private readonly IStaticDataService _staticDataService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IUiFactory uiFactory, IScanningLineFactory scanningLineFactory, IParabolaFactory parabolaFactory, IWindowService windowService, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _scanningLineFactory = scanningLineFactory;
            _parabolaFactory = parabolaFactory;
            _windowService = windowService;
            _staticDataService = staticDataService;
        }

        public void Enter(string scene) =>
            _sceneLoader.Load(scene, OnLoaded);

        private void OnLoaded()
        {
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == GlobalMap)
            {
                //TODO: Static data for sites count
                int sitesCount = 4;

                _parabolaFactory.CreateAndStoreParabolas(sitesCount);
                _scanningLineFactory.CreateScanningLine();
            }
            else
            {
                _staticDataService.Load();
                _uiFactory.CreateUiRoot();
                OpenChooseCommandWindow();
            }
        }

        private void OpenChooseCommandWindow() =>
            _windowService.OpenWindow(WindowType.ChooseCommand);

        public void Exit()
        {
        }
    }
}