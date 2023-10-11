using Assets.CodeBase.Infrastructure.Services.Factory.Ui;
using Assets.CodeBase.Infrastructure.Services.StaticData;

namespace Assets.CodeBase.Infrastructure.Services.Factory.HumanControlTools
{
    public class HumanControlUiFactory : IHumanControlUiFactory
    {
        private readonly IUiFactory _uiFactory;
        private readonly IStaticDataService _staticDataService;

        public HumanControlUiFactory(IUiFactory uiFactory, IStaticDataService staticDataService)
        {
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
        }

        public void CreateHumanControlledTools(CommandColor commandColor) =>
            _uiFactory.CreateHumanControlledUi(commandColor);
    }
}
