using Assets.CodeBase.Logic.RedCommand;
using UnityEngine;

public class EnableMeleeAttackOnEnemyInFrontForGreenCommand : MonoBehaviour
{
    [SerializeField] private GreenCommandMeleeUnitAttack _greenComandMeleeUnitAttack;
    [SerializeField] private ClosestEnemyUnitCalculatorForGreenCommand _closestEnemyCalculator;
    [SerializeField] private float _attackRange;

    private void Update()
    {
        (float distance, RedCommandUnit unit) closestRedUnit = _closestEnemyCalculator.ClosestRedCommandUnit();

        if (closestRedUnit.distance < _attackRange)
            _greenComandMeleeUnitAttack.EnableAttack();

        else
            _greenComandMeleeUnitAttack.DisableAttack();
    }
}
