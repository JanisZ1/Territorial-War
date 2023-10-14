using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using Assets.CodeBase.Infrastructure.Services.Factory.Spawners;
using Assets.CodeBase.Infrastructure.Services.StaticData;
using Assets.CodeBase.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory.Ui
{
    public class UiFactory : IUiFactory
    {
        private readonly IAssets _assets;
        private readonly IHumanUnitSpawnerFactory _humanUnitSpawnerFactory;
        private readonly IStaticDataService _staticDataService;
        private Transform _uiRoot;

        public UiFactory(IAssets assets, IHumanUnitSpawnerFactory humanUnitSpawnerFactory, IStaticDataService staticDataService)
        {
            _assets = assets;
            _humanUnitSpawnerFactory = humanUnitSpawnerFactory;
            _staticDataService = staticDataService;
        }

        public void CreateUiRoot() =>
            _uiRoot = _assets.Instantiate(AssetPath.UiRootPath).transform;

        public void CreateHumanControlledUi(WindowType windowType)
        {
            WindowStaticData windowData = _staticDataService.ForWindow(windowType);

            GameObject window = Object.Instantiate(windowData.Prefab, _uiRoot);

            foreach (QueueUnit queueUnit in window.GetComponentsInChildren<QueueUnit>())
                queueUnit.Construct(_humanUnitSpawnerFactory);
        }

        public GameObject CreateWindow(WindowType windowType)
        {
            WindowStaticData windowData = _staticDataService.ForWindow(windowType);
            GameObject window = Object.Instantiate(windowData.Prefab, _uiRoot);

            return window;
        }
    }
}
