using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using Assets.CodeBase.Infrastructure.Services.StaticData;
using Assets.CodeBase.Logic.Ui;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory.Ui
{
    public class UiFactory : IUiFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticDataService;
        private Transform _uiRoot;

        public UiFactory(IAssets assets, IStaticDataService staticDataService)
        {
            _assets = assets;
            _staticDataService = staticDataService;
        }

        public void CreateUiRoot() =>
            _uiRoot = _assets.Instantiate(AssetPath.UiRootPath).transform;

        public GameObject CreateChooseCommandButtons()
        {
            GameObject chooseCommandButtons = _assets.Instantiate(AssetPath.ChooseCommandUiPath, _uiRoot);
            return chooseCommandButtons;
        }

        public void CreateQueueButtons(CommandColor commandColor)
        {
        }

        public void CreateUpgradeButtons(CommandColor commandColor)
        {
        }
    }
}
