using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.AiUnitControll;
using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using Assets.CodeBase.Infrastructure.Services.ChooseCommandMediator;
using Assets.CodeBase.Infrastructure.Services.Factory.Spawners;
using Assets.CodeBase.Infrastructure.Services.Factory.Ui;
using Assets.CodeBase.Infrastructure.Services.Factory.Unit;
using Assets.CodeBase.Infrastructure.Services.GreenCommandUnitsHandler;
using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.Infrastructure.Services.StaticData;
using Assets.CodeBase.Infrastructure.Services.Window;
using Assets.CodeBase.Infrastructure.StateMachine;

namespace Assets.CodeBase.Infrastructure
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ICoroutinerRunner _coroutinerRunner;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        private const string Bootstrap = "Bootstrap";
        private const string Main = "Main";

        public BootstrapState(GameStateMachine gameStateMachine, ICoroutinerRunner coroutinerRunner, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _coroutinerRunner = coroutinerRunner;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter() =>
            _sceneLoader.Load(Bootstrap, EnterLoadLevel);

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.Register<IAssets>(new AssetProvider());
            _services.Register<IStaticDataService>(new StaticDataService());
            _services.Register<IRedCommandUnitsHandler>(new RedCommandUnitsHandler());
            _services.Register<IGreenCommandUnitsHandler>(new GreenCommandUnitsHandler());
            _services.Register<IUnitFactory>(new UnitFactory(_services.Single<IStaticDataService>()));
            _services.Register<IHumanUnitSpawnerFactory>(new HumanUnitSpawnerFactory(_services.Single<IStaticDataService>(), _services.Single<IUnitFactory>(), _services.Single<IRedCommandUnitsHandler>(), _services.Single<IGreenCommandUnitsHandler>()));
            _services.Register<IAiUnitSpawnerFactory>(new AiUnitSpawnerFactory(_services.Single<IStaticDataService>(), _services.Single<IUnitFactory>(), _services.Single<IRedCommandUnitsHandler>(), _services.Single<IGreenCommandUnitsHandler>()));
            _services.Register<IUiFactory>(new UiFactory(_services.Single<IAssets>(), _services.Single<IHumanUnitSpawnerFactory>(), _services.Single<IStaticDataService>()));
            _services.Register<IAiUnitSpawnControll>(new AiUnitSpawnControll(_coroutinerRunner, _services.Single<IAiUnitSpawnerFactory>()));
            _services.Register<IChooseCommandMediator>(new ChooseCommandMediator(_services.Single<IUiFactory>(), _services.Single<IHumanUnitSpawnerFactory>(), _services.Single<IAiUnitSpawnerFactory>(), _services.Single<IAiUnitSpawnControll>()));
            _services.Register<IWindowService>(new WindowService(_services.Single<IUiFactory>(), _services.Single<IChooseCommandMediator>()));
        }

        private void EnterLoadLevel() =>
            _gameStateMachine.Enter<LoadLevelState>(Main);
    }
}