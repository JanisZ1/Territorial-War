using Assets.CodeBase.Infrastructure.Services.AiUnitControll;
using Assets.CodeBase.Infrastructure.Services.Factory.Spawners;
using Assets.CodeBase.Infrastructure.Services.Factory.Ui;

namespace Assets.CodeBase.Infrastructure.Services.ChooseCommandMediator
{
    public class ChooseCommandMediator : IChooseCommandMediator
    {
        private readonly IUiFactory _uiFactory;
        private readonly IHumanUnitSpawnerFactory _humanSpawnerFactory;
        private readonly IAiUnitSpawnerFactory _aiUnitSpawnerFactory;
        private readonly IAiUnitSpawnControll _aiUnitSpawnControll;

        public ChooseCommandMediator(IUiFactory uiFactory, IHumanUnitSpawnerFactory humanSpawnerFactory, IAiUnitSpawnerFactory aiUnitSpawnerFactory, IAiUnitSpawnControll aiUnitSpawnControll)
        {
            _uiFactory = uiFactory;
            _humanSpawnerFactory = humanSpawnerFactory;
            _aiUnitSpawnerFactory = aiUnitSpawnerFactory;
            _aiUnitSpawnControll = aiUnitSpawnControll;
        }

        public void ChooseCommand(CommandColor commandColor)
        {
            _uiFactory.CreateHumanControlledUi(commandColor);
            _humanSpawnerFactory.CreateCommandSpawner(commandColor);

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

        private void StartAiGreenUnitsSpawn()
        {
            _aiUnitSpawnerFactory.CreateCommandSpawner(CommandColor.Green);
            _aiUnitSpawnControll.StartSpawnTimer(CommandColor.Green);
        }

        private void StartAiRedUnitsSpawn()
        {
            _aiUnitSpawnerFactory.CreateCommandSpawner(CommandColor.Red);
            _aiUnitSpawnControll.StartSpawnTimer(CommandColor.Red);
        }
    }
}
