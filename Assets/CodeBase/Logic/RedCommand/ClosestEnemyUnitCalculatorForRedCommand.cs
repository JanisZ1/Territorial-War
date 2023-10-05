using Assets.CodeBase.Infrastructure.Services.GreenCommandUnitsHandler;
using Assets.CodeBase.Logic.GreenCommand;
using UnityEngine;

namespace Assets.CodeBase.Logic.RedCommand
{
    public class ClosestEnemyUnitCalculatorForRedCommand : MonoBehaviour
    {
        private IGreenCommandUnitsHandler _greenCommandUnitsHandler;

        public void Construct(IGreenCommandUnitsHandler greenCommandUnitsHandler) =>
            _greenCommandUnitsHandler = greenCommandUnitsHandler;

        public (float distance, GreenCommandUnit unit) ClosestGreenCommandUnit()
        {
            float minDistance = float.MaxValue;
            GreenCommandUnit minClosestGreenCommandUnit = null;

            foreach (GreenCommandUnit redCommandUnit in _greenCommandUnitsHandler.GreenCommandUnits)
            {
                float distance = Vector3.Distance(transform.position, redCommandUnit.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minClosestGreenCommandUnit = redCommandUnit;
                }
            }

            return (minDistance, minClosestGreenCommandUnit);
        }
    }
}
