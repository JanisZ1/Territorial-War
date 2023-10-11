using Assets.CodeBase.Infrastructure.Services.AssetProvider;

namespace Assets.CodeBase.Infrastructure.Services.Factory.Spawners
{
    public class SpawnersFactory : ISpawnersFactory
    {
        private readonly IAssets _assets;

        public SpawnersFactory(IAssets assets) =>
            _assets = assets;

        public void CreateCommandSpawner(CommandColor commandColor)
        {
            switch (commandColor)
            {
                case CommandColor.Green:
                    _assets.Instantiate(AssetPath.GreenCommandSpawner);
                    break;
                case CommandColor.Red:
                    _assets.Instantiate(AssetPath.RedCommandSpawner);
                    break;
            }
        }
    }
}
