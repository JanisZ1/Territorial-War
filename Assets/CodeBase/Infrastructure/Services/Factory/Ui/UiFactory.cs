using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using Assets.CodeBase.Infrastructure.Services.Factory.Spawners;
using Assets.CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory.Ui
{
    public class UiFactory : IUiFactory
    {
        private readonly IAssets _assets;
        private readonly IHumanUnitSpawnerFactory _spawnersFactory;
        private readonly IStaticDataService _staticDataService;
        private Transform _uiRoot;

        public UiFactory(IAssets assets, IHumanUnitSpawnerFactory spawnersFactory, IStaticDataService staticDataService)
        {
            _assets = assets;
            _spawnersFactory = spawnersFactory;
            _staticDataService = staticDataService;
        }

        public void CreateUiRoot() =>
            _uiRoot = _assets.Instantiate(AssetPath.UiRootPath).transform;

        public GameObject CreateChooseCommandButtons()
        {
            GameObject chooseCommandButtons = _assets.Instantiate(AssetPath.ChooseCommandUiPath, _uiRoot);
            return chooseCommandButtons;
        }

        public void CreateHumanControlledUi(CommandColor commandColor)
        {
            switch (commandColor)
            {
                case CommandColor.Green:
                    GameObject greenUigameObject = _assets.Instantiate(AssetPath.GreenCommandHumanUiPath, _uiRoot);

                    foreach (QueueUnit queueUnit in greenUigameObject.GetComponentsInChildren<QueueUnit>())
                    {
                        queueUnit.Construct(_spawnersFactory);
                    }

                    break;

                case CommandColor.Red:
                    GameObject redUigameObject = _assets.Instantiate(AssetPath.RedCommandHumanUiPath, _uiRoot);

                    foreach (QueueUnit queueUnit in redUigameObject.GetComponentsInChildren<QueueUnit>())
                    {
                        queueUnit.Construct(_spawnersFactory);
                    }

                    break;
            }
        }
    }
}
