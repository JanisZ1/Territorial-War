using Assets.CodeBase.Infrastructure.Services.Factory.Ui;

namespace Assets.CodeBase.Infrastructure.Services.Factory.HumanControlTools
{
    public class HumanControlUiFactory : IHumanControlUiFactory
    {
        private readonly IUiFactory _uiFactory;

        public HumanControlUiFactory(IUiFactory uiFactory) =>
            _uiFactory = uiFactory;

        public void CreateHumanControlledTools(CommandColor commandColor) =>
            _uiFactory.CreateHumanControlledUi(commandColor);
    }
}
