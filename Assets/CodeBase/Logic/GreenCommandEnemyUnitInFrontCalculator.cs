using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.Logic.RedCommand;
using UnityEngine;

public class GreenCommandEnemyUnitInFrontCalculator : MonoBehaviour
{
    private IRedCommandUnitsHandler _redCommandUnitsHandler;

    public void Construct(IRedCommandUnitsHandler redCommandUnitsHandler) =>
        _redCommandUnitsHandler = redCommandUnitsHandler;

    public (float distance, RedCommandUnit unit) CalculateClosestEnemyForMelee() =>
        GetClosestRedCommandUnit();

    private (float distance, RedCommandUnit unit) GetClosestRedCommandUnit()
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