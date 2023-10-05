using Assets.CodeBase.Logic.GreenCommand;
using UnityEngine;

namespace Assets.CodeBase.Logic.RedCommand
{
    public class StopMoveOnEnemyInFrontForRedCommand : MonoBehaviour
    {
        [SerializeField] private RedCommandMeleeUnitMove _redCommandMeleeUnitMove;
        [SerializeField] private ClosestEnemyUnitCalculatorForRedCommand _closestEnemyCalculator;
        [SerializeField] private float _stopMoveDistance;

        private void Update()
        {
            (float distance, GreenCommandUnit unit) closestRedUnit = _closestEnemyCalculator.ClosestGreenCommandUnit();

            if (closestRedUnit.distance < _stopMoveDistance)
                _redCommandMeleeUnitMove.StopMove();

            else
                _redCommandMeleeUnitMove.StartMove();
        }
    }
}
