using Assets.CodeBase.Logic.GreenCommand;
using UnityEngine;
using GreenCommandAnimationState = Assets.CodeBase.Logic.GreenCommand.GreenCommandAnimationState;

public class StopMoveOnAttackForGreenCommand : MonoBehaviour
{
    [SerializeField] private GreenCommandMeleeUnitMove _greenCommandMeleeUnitMove;
    [SerializeField] private GreenCommandAnimator _greenCommandAnimator;

    private void Start()
    {
        _greenCommandAnimator.OnStateExited += StartMove;
        _greenCommandAnimator.OnStateEntered += StopMove;
    }

    private void OnDestroy()
    {
        _greenCommandAnimator.OnStateExited -= StartMove;
        _greenCommandAnimator.OnStateEntered -= StopMove;
    }

    private void StartMove(GreenCommandAnimationState animationState)
    {
        if (animationState == GreenCommandAnimationState.Attack)
            _greenCommandMeleeUnitMove.StartMove();
    }

    private void StopMove(GreenCommandAnimationState animationState)
    {
        if (animationState == GreenCommandAnimationState.Attack)
            _greenCommandMeleeUnitMove.StopMove();
    }
}
