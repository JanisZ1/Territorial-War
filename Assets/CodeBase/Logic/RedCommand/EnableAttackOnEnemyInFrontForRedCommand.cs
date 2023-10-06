using Assets.CodeBase.Logic.GreenCommand;
using UnityEngine;

namespace Assets.CodeBase.Logic.RedCommand
{
    public class EnableAttackOnEnemyInFrontForRedCommand : MonoBehaviour
    {
        [SerializeField] private RedComandMeleeUnitAttack _redComandMeleeUnitAttack;
        [SerializeField] private ClosestEnemyUnitCalculatorForRedCommand _closestEnemyCalculator;
        [SerializeField] private float _attackRange;

        private void Update()
        {
            (float distance, GreenCommandUnit unit) closestUnit = _closestEnemyCalculator.ClosestGreenCommandUnit();

            if (closestUnit.distance < _attackRange)
                _redComandMeleeUnitAttack.EnableAttack();

            else
                _redComandMeleeUnitAttack.DisableAttack();
        }
    }
}
