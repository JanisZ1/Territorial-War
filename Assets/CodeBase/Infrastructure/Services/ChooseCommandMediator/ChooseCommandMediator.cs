using Assets.CodeBase.Infrastructure.Services.AiUnitControll;
using Assets.CodeBase.Infrastructure.Services.Factory.Spawners;
using Assets.CodeBase.Infrastructure.Services.Factory.Ui;
using Assets.CodeBase.Logic.Ui;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.ChooseCommandMediator
{
    public class ChooseCommandMediator : IChooseCommandMediator
    {
        private readonly IUiFactory _uiFactory;
        private readonly IHumanUnitSpawnerFactory _humanSpawnerFactory;
        private readonly IAiUnitSpawnerFactory _aiUnitSpawnerFactory;
        private readonly IAiUnitSpawnControll _aiUnitSpawnControll;
        private GameObject _window;

        public ChooseCommandMediator(IUiFactory uiFactory, IHumanUnitSpawnerFactory humanSpawnerFactory, IAiUnitSpawnerFactory aiUnitSpawnerFactory, IAiUnitSpawnControll aiUnitSpawnControll)
        {
            _uiFactory = uiFactory;
            _humanSpawnerFactory = humanSpawnerFactory;
            _aiUnitSpawnerFactory = aiUnitSpawnerFactory;
            _aiUnitSpawnControll = aiUnitSpawnControll;
        }

        public void SubscribeToChooseCommand(GameObject window)
        {
            _window = window;

            foreach (ChooseCommandButton chooseButton in window.GetComponentsInChildren<ChooseCommandButton>())
                chooseButton.CommandColorChoosed += CommandColorChoosed;
        }

        private void CommandColorChoosed(CommandColor commandColor)
        {
            CreateHumanControlTools(commandColor);
            CreateOppositeAiControlToolsOf(commandColor);
            UnSubscribeFromChooseCommand();
        }

        private void UnSubscribeFromChooseCommand()
        {
            foreach (ChooseCommandButton chooseButton in _window.GetComponentsInChildren<ChooseCommandButton>())
                chooseButton.CommandColorChoosed -= CommandColorChoosed;
        }

        private void CreateOppositeAiControlToolsOf(CommandColor commandColor)
        {
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

        private void CreateHumanControlTools(CommandColor commandColor)
        {
            _uiFactory.CreateHumanControlledUi(commandColor);
            _humanSpawnerFactory.CreateCommandSpawner(commandColor);
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
