using Assets.CodeBase.Infrastructure.Services.Factory.Unit;
using Assets.CodeBase.Infrastructure.Services.GreenCommandUnitsHandler;
using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.Infrastructure.Services.StaticData;
using Assets.CodeBase.Logic.Spawners;
using Assets.CodeBase.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory.Spawners
{
    public class AiUnitSpawnerFactory : IAiUnitSpawnerFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IUnitFactory _unitFactory;
        private readonly IRedCommandUnitsHandler _redCommandUnitsHandler;
        private readonly IGreenCommandUnitsHandler _greenCommandUnitsHandler;

        public UnitSpawner UnitSpawner { get; private set; }

        public AiUnitSpawnerFactory(IStaticDataService staticDataService, IUnitFactory unitFactory, IRedCommandUnitsHandler redCommandUnitsHandler, IGreenCommandUnitsHandler greenCommandUnitsHandler)
        {
            _staticDataService = staticDataService;
            _unitFactory = unitFactory;
            _redCommandUnitsHandler = redCommandUnitsHandler;
            _greenCommandUnitsHandler = greenCommandUnitsHandler;
        }

        public GameObject CreateCommandSpawner(CommandColor commandColor)
        {
            SpawnerStaticData spawnerStaticData = _staticDataService.ForSpawner(commandColor);

            UnitSpawner = Object.Instantiate(spawnerStaticData.Prefab).GetComponent<UnitSpawner>();

            //TODO: Delete code dublicate in this and human spawner factory
            switch (commandColor)
            {
                case CommandColor.Green:
                    UnitSpawner.GetComponent<GreenCommandUnitSpawner>().Construct(_unitFactory, _redCommandUnitsHandler, _greenCommandUnitsHandler);
                    break;
                case CommandColor.Red:
                    UnitSpawner.GetComponent<RedCommandUnitSpawner>().Construct(_unitFactory, _redCommandUnitsHandler, _greenCommandUnitsHandler);
                    break;
            }

            return UnitSpawner.gameObject;
        }
    }
}
