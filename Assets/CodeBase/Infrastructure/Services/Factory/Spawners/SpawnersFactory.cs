using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory.Spawners
{
    public class SpawnersFactory : ISpawnersFactory
    {
        private readonly IAssets _assets;

        public SpawnersFactory(IAssets assets) =>
            _assets = assets;

        public GameObject CreateCommandSpawner(CommandColor commandColor)
        {
            switch (commandColor)
            {
                case CommandColor.Green:
                    return _assets.Instantiate(AssetPath.GreenCommandSpawner);

                case CommandColor.Red:
                    return _assets.Instantiate(AssetPath.RedCommandSpawner);
            }
            return null;
        }
    }
}
