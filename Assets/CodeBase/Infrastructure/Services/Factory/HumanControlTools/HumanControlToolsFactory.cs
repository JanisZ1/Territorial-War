using Assets.CodeBase.Infrastructure.Services.Factory.Ui;
using Assets.CodeBase.Infrastructure.Services.StaticData;

namespace Assets.CodeBase.Infrastructure.Services.Factory.HumanControlTools
{
    public class HumanControlToolsFactory : IHumanControlToolsFactory
    {
        private readonly IUiFactory _uiFactory;
        private readonly IStaticDataService _staticDataService;

        public HumanControlToolsFactory(IUiFactory uiFactory, IStaticDataService staticDataService)
        {
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
        }

        public void CreateHumanControlledTools(CommandColor commandColor)
        {
            _uiFactory.CreateQueueButtons(commandColor);
            _uiFactory.CreateUpgradeButtons(commandColor);
        }
    }
}
