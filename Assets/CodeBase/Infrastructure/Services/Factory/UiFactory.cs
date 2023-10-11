using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory
{
    public class UiFactory : IUiFactory
    {
        private readonly IAssets _assets;
        private Transform _uiRoot;

        public UiFactory(IAssets assets) =>
            _assets = assets;

        public void CreateUiRoot() =>
            _uiRoot = _assets.Instantiate(AssetPath.UiRootPath).transform;

        public void CreateChooseButtons()
        {

        }

        public void CreateQueueButtons(CommandColor commandColor)
        {
        }

        public void CreateUpgradeButtons(CommandColor commandColor)
        {
        }
    }
}
