using Assets.CodeBase.Logic.GreenCommand;
using UnityEngine;

public class StopMoveOnEnemyInFrontForGreenCommand : MonoBehaviour
{
    [SerializeField] private GreenCommandMeleeUnitMove _greenCommandMeleeUnitMove;
    [SerializeField] private ClosestEnemyUnitCalculatorForGreenCommand _closestEnemyCalculator;
    [SerializeField] private float _stopMoveDistance;

    private void Update()
    {
        (float distance, Assets.CodeBase.Logic.RedCommand.RedCommandUnit unit) closestRedUnit = _closestEnemyCalculator.ClosestRedCommandUnit();

        if (closestRedUnit.distance < _stopMoveDistance)
            _greenCommandMeleeUnitMove.StopMove();

        else
            _greenCommandMeleeUnitMove.StartMove();
    }
}
