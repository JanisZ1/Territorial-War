using Assets.CodeBase.Infrastructure.Services.AiUnitControll;
using Assets.CodeBase.Infrastructure.Services.Factory.HumanControlTools;

namespace Assets.CodeBase.Infrastructure.Services.ChooseCommandMediator
{
    public class ChooseCommandMediator : IChooseCommandMediator
    {
        private readonly IHumanControlUiFactory _humanControlToolsFactory;
        private readonly IAiUnitSpawnControll _aiUnitSpawnControll;

        public ChooseCommandMediator(IHumanControlUiFactory humanControlToolsFactory, IAiUnitSpawnControll aiUnitSpawnControll)
        {
            _humanControlToolsFactory = humanControlToolsFactory;
            _aiUnitSpawnControll = aiUnitSpawnControll;
        }

        public void ChooseCommand(CommandColor commandColor)
        {
            _humanControlToolsFactory.CreateHumanControlledTools(commandColor);

            switch (commandColor)
            {
                case CommandColor.Green:
                    StartAiRedUnitsSpawn();
                    break;

                case CommandColor.Red:
                    StartAiGreenUnitsSpawn();
                    break;
            }
        }

        private void StartAiGreenUnitsSpawn() =>
            _aiUnitSpawnControll.StartSpawnTimer(CommandColor.Green);

        private void StartAiRedUnitsSpawn() =>
            _aiUnitSpawnControll.StartSpawnTimer(CommandColor.Red);
    }
}
