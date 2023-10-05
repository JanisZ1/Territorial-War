using Assets.CodeBase.Infrastructure.Services.GreenCommandUnitsHandler;
using Assets.CodeBase.Logic.GreenCommand;
using UnityEngine;

namespace Assets.CodeBase.Logic.RedCommand
{
    public class CheckAttackRangeForRedCommandMelee : MonoBehaviour
    {
        private IGreenCommandUnitsHandler _greemCommandUnitsHandler;
        [SerializeField] private RedComandMeleeUnitAttack _redComandMeleeUnitAttack;
        [SerializeField] private float _attackRange;

        public void Construct(IGreenCommandUnitsHandler greenCommandUnitsHandler) =>
            _greemCommandUnitsHandler = greenCommandUnitsHandler;

        private void Update()
        {
            (float distance, GreenCommandUnit unit) closestUnit = ClosestGreenCommandUnit();

            if (closestUnit.distance < _attackRange)
                _redComandMeleeUnitAttack.EnableAttack();

            else
                _redComandMeleeUnitAttack.DisableAttack();
        }

        private (float distance, GreenCommandUnit unit) ClosestGreenCommandUnit()
        {
            float minDistance = float.MaxValue;
            GreenCommandUnit minClosestGreenCommandUnit = null;

            foreach (GreenCommandUnit greenCommandUnit in _greemCommandUnitsHandler.GreenCommandUnits)
            {
                float distance = Vector3.Distance(transform.position, greenCommandUnit.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minClosestGreenCommandUnit = greenCommandUnit;
                }
            }

            return (minDistance, minClosestGreenCommandUnit);
        }
    }
}
