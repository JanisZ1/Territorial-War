using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.Logic.RedCommand;
using UnityEngine;

public class CheckAttackRangeForGreenCommandMelee : MonoBehaviour
{
    private IRedCommandUnitsHandler _redCommandUnitsHandler;
    [SerializeField] private GreenComandMeleeUnitAttack _greenComandMeleeUnitAttack;
    [SerializeField] private float _attackRange;

    public void Construct(IRedCommandUnitsHandler redCommandUnitsHandler) =>
        _redCommandUnitsHandler = redCommandUnitsHandler;

    private void Update()
    {
        (float distance, RedCommandUnit unit) closestUnit = ClosestRedCommandUnit();

        if (closestUnit.distance < _attackRange)
            _greenComandMeleeUnitAttack.EnableAttack();

        else
            _greenComandMeleeUnitAttack.DisableAttack();
    }

    private (float distance, RedCommandUnit unit) ClosestRedCommandUnit()
    {
        float minDistance = float.MaxValue;
        RedCommandUnit minClosestRedCommandUnit = null;

        foreach (RedCommandUnit redCommandUnit in _redCommandUnitsHandler.RedCommandUnits)
        {
            float distance = Vector3.Distance(transform.position, redCommandUnit.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                minClosestRedCommandUnit = redCommandUnit;
            }
        }

        return (minDistance, minClosestRedCommandUnit);
    }
}