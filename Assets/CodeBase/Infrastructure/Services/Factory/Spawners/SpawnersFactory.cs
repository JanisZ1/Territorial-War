using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using Assets.CodeBase.Infrastructure.Services.StaticData;
using Assets.CodeBase.Logic.Spawners;
using Assets.CodeBase.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory.Spawners
{
    public class SpawnersFactory : ISpawnersFactory
    {
        private readonly IStaticDataService _staticDataService;

        public UnitSpawner UnitSpawner { get; private set; }

        public SpawnersFactory(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        public GameObject CreateCommandSpawner(CommandColor commandColor)
        {
            SpawnerStaticData spawnerStaticData = _staticDataService.ForSpawner(commandColor);

            UnitSpawner = Object.Instantiate(spawnerStaticData.Prefab).GetComponent<UnitSpawner>();

            return UnitSpawner.gameObject;
        }
    }
}
