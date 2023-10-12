using Assets.CodeBase.Infrastructure.Services.Factory.Spawners;
using Assets.CodeBase.Infrastructure.Services.Factory.Ui;
using Assets.CodeBase.Infrastructure.Services.StaticData;

namespace Assets.CodeBase.Infrastructure.Services.Factory.HumanControlTools
{
    public class HumanControlUiFactory : IHumanControlUiFactory
    {
        private readonly IUiFactory _uiFactory;
        private readonly IHumanSpawnerFactory _humanSpawnerFactory;
        private readonly IStaticDataService _staticDataService;

        public HumanControlUiFactory(IUiFactory uiFactory, IHumanSpawnerFactory humanSpawnerFactory, IStaticDataService staticDataService)
        {
            _uiFactory = uiFactory;
            _humanSpawnerFactory = humanSpawnerFactory;
            _staticDataService = staticDataService;
        }

        public void CreateHumanControlledTools(CommandColor commandColor)
        {
            _humanSpawnerFactory.CreateCommandSpawner(commandColor);
            _uiFactory.CreateHumanControlledUi(commandColor);
        }
    }
}
