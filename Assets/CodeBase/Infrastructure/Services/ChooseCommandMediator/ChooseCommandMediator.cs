using Assets.CodeBase.Infrastructure.Services.Factory.HumanControlTools;

namespace Assets.CodeBase.Infrastructure.Services.ChooseCommandMediator
{
    public class ChooseCommandMediator : IChooseCommandMediator
    {
        private readonly IHumanControlToolsFactory _humanControlToolsFactory;

        public ChooseCommandMediator(IHumanControlToolsFactory humanControlToolsFactory) =>
            _humanControlToolsFactory = humanControlToolsFactory;

        public void ChooseCommand(CommandColor commandColor) =>
            _humanControlToolsFactory.CreateHumanControlledTools(commandColor);
    }
}
