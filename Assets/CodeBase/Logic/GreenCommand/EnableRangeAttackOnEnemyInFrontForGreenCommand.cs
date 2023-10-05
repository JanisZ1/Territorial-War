using Assets.CodeBase.Logic.RedCommand;
using UnityEngine;

public class EnableRangeAttackOnEnemyInFrontForGreenCommand : MonoBehaviour
{
    [SerializeField] private GreenCommandRangeUnitAttack _greenCommandRangeUnitAttack;
    [SerializeField] private ClosestEnemyUnitCalculatorForGreenCommand _closestEnemyCalculator;
    [SerializeField] private float _minAttackRange;
    [SerializeField] private float _maxAttackRange;

    private void Update()
    {
        (float distance, RedCommandUnit unit) closestRedUnit = _closestEnemyCalculator.ClosestRedCommandUnit();

        if (closestRedUnit.distance < _maxAttackRange && closestRedUnit.distance > _minAttackRange)
            _greenCommandRangeUnitAttack.EnableAttack();

        else
            _greenCommandRangeUnitAttack.DisableAttack();
    }
}
