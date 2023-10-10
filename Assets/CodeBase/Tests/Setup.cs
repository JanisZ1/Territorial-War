using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using UnityEngine;

namespace Assets.CodeBase.Tests
{
    public class Setup
    {
        public static float InitialPosition(Transform greenCommandUnit) =>
            greenCommandUnit.transform.position.x;

        public static Transform GreenCommandUnit()
        {
            IRedCommandUnitsHandler redCommandUnitsHandler = Create.RedCommandUnitsHandler();
            Transform greenCommandUnit = Create.GreenCommandUnit(redCommandUnitsHandler);

            return greenCommandUnit;
        }
    }
}
