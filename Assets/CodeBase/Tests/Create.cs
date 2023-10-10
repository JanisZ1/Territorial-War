using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.Logic.RedCommand;
using NSubstitute;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Tests
{
    public class Create
    {
        public static Transform GreenCommandUnit(IRedCommandUnitsHandler redCommandUnitsHandler)
        {
            Transform greenCommandUnit = GreenCommandUnit();
            ClosestEnemyUnitCalculatorForGreenCommand closestEnemyCalculator = greenCommandUnit.GetComponent<ClosestEnemyUnitCalculatorForGreenCommand>();
            closestEnemyCalculator.Construct(redCommandUnitsHandler);

            return greenCommandUnit;
        }

        public static IRedCommandUnitsHandler RedCommandUnitsHandler()
        {
            IRedCommandUnitsHandler redCommandUnitsHandler = Substitute.For<IRedCommandUnitsHandler>();
            redCommandUnitsHandler.RedCommandUnits = new List<RedCommandUnit>();
            return redCommandUnitsHandler;
        }

        private static Transform GreenCommandUnit() =>
            Object.Instantiate(Resources.Load<Transform>(AssetPath.GreenCommandWarriorPath));
    }
}
