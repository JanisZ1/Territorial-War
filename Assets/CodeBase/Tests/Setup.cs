using Assets.CodeBase.Infrastructure.Services.GreenCommandUnitsHandler;
using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.Logic.GreenCommand;
using Assets.CodeBase.Logic.RedCommand;

namespace Assets.CodeBase.Tests
{
    public class Setup
    {
        public static GreenCommandMeleeUnitMove GreenCommandMeleeUnitMove(RedCommandUnit redCommandUnit = null)
        {
            IRedCommandUnitsHandler redCommandUnitsHandler = Create.RedCommandUnitsHandler(redCommandUnit);
            GreenCommandMeleeUnitMove greenCommandUnit = Create.GreenCommandMeleeUnitMove(redCommandUnitsHandler);

            return greenCommandUnit;
        }

        public static RedCommandMeleeUnitMove RedCommandMeleeUnitMove(GreenCommandUnit greenCommandUnit = null)
        {
            IGreenCommandUnitsHandler greenCommandUnitsHandler = Create.GreenCommandUnitsHandler(greenCommandUnit);
            RedCommandMeleeUnitMove redCommandUnit = Create.RedCommandMeleeUnitMove(greenCommandUnitsHandler);

            return redCommandUnit;
        }
    }
}
