using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using Assets.CodeBase.Infrastructure.Services.GreenCommandUnitsHandler;
using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.Logic.GreenCommand;
using Assets.CodeBase.Logic.RedCommand;
using NSubstitute;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Tests
{
    public class Create
    {
        public static RedCommandMeleeUnitMove RedCommandMeleeUnitMove(IGreenCommandUnitsHandler greenCommandUnitsHandler)
        {
            RedCommandMeleeUnitMove redCommandUnit = RedCommandUnit();
            ClosestEnemyUnitCalculatorForRedCommand closestEnemyCalculator = redCommandUnit.GetComponent<ClosestEnemyUnitCalculatorForRedCommand>();
            closestEnemyCalculator.Construct(greenCommandUnitsHandler);

            return redCommandUnit;
        }

        public static GreenCommandMeleeUnitMove GreenCommandMeleeUnitMove(IRedCommandUnitsHandler redCommandUnitsHandler)
        {
            GreenCommandMeleeUnitMove greenCommandUnit = GreenCommandUnit();
            ClosestEnemyUnitCalculatorForGreenCommand closestEnemyCalculator = greenCommandUnit.GetComponent<ClosestEnemyUnitCalculatorForGreenCommand>();
            closestEnemyCalculator.Construct(redCommandUnitsHandler);

            return greenCommandUnit;
        }

        public static IGreenCommandUnitsHandler GreenCommandUnitsHandler(GreenCommandUnit greenCommandUnit = null)
        {
            IGreenCommandUnitsHandler greenCommandUnitsHandler = Substitute.For<IGreenCommandUnitsHandler>();
            greenCommandUnitsHandler.GreenCommandUnits = new List<GreenCommandUnit>();

            if (greenCommandUnit)
                greenCommandUnitsHandler.GreenCommandUnits.Add(greenCommandUnit);

            return greenCommandUnitsHandler;
        }

        public static IRedCommandUnitsHandler RedCommandUnitsHandler(RedCommandUnit redCommandUnit = null)
        {
            IRedCommandUnitsHandler redCommandUnitsHandler = Substitute.For<IRedCommandUnitsHandler>();
            redCommandUnitsHandler.RedCommandUnits = new List<RedCommandUnit>();

            if (redCommandUnit)
                redCommandUnitsHandler.RedCommandUnits.Add(redCommandUnit);

            return redCommandUnitsHandler;
        }

        private static GreenCommandMeleeUnitMove GreenCommandUnit() =>
            Object.Instantiate(Resources.Load<GreenCommandMeleeUnitMove>(AssetPath.GreenCommandWarriorPath));

        private static RedCommandMeleeUnitMove RedCommandUnit() =>
            Object.Instantiate(Resources.Load<RedCommandMeleeUnitMove>(AssetPath.RedCommandWarriorPath));
    }
}
