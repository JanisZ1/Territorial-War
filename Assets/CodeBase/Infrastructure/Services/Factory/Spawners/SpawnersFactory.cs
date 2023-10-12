using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using Assets.CodeBase.Logic.Spawners;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory.Spawners
{
    public class SpawnersFactory : ISpawnersFactory
    {
        private readonly IAssets _assets;

        public GreenCommandUnitSpawner GreenCommandUnitSpawner { get; private set; }

        public RedCommandUnitSpawner RedCommandUnitSpawner { get; private set; }

        public SpawnersFactory(IAssets assets) =>
            _assets = assets;

        public GameObject CreateCommandSpawner(CommandColor commandColor)
        {
            switch (commandColor)
            {
                case CommandColor.Green:
                    GreenCommandUnitSpawner = _assets.Instantiate(AssetPath.GreenCommandSpawner).GetComponent<GreenCommandUnitSpawner>();
                    return GreenCommandUnitSpawner.gameObject;

                case CommandColor.Red:
                    RedCommandUnitSpawner = _assets.Instantiate(AssetPath.RedCommandSpawner).GetComponent<RedCommandUnitSpawner>();
                    return RedCommandUnitSpawner.gameObject;
            }
            return null;
        }
    }
}
