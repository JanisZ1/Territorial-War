using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.Logic.RedCommand;
using UnityEngine;

namespace Assets.CodeBase.Tests
{
    public class Setup
    {
        public static float InitialPosition(Transform greenCommandUnit) =>
            greenCommandUnit.transform.position.x;

        public static Transform GreenCommandUnit(RedCommandUnit redCommandUnit = null)
        {
            IRedCommandUnitsHandler redCommandUnitsHandler = Create.RedCommandUnitsHandler(redCommandUnit);
            Transform greenCommandUnit = Create.GreenCommandUnit(redCommandUnitsHandler);

            return greenCommandUnit;
        }
    }
}
