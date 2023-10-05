using Assets.CodeBase.Logic.GreenCommand;
using UnityEngine;
using AnimationState = Assets.CodeBase.Logic.GreenCommand.AnimationState;

public class StopMoveOnAttack : MonoBehaviour
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

    private void StartMove(AnimationState animationState)
    {
        if (animationState == AnimationState.Attack)
            _greenCommandMeleeUnitMove.StartMove();
    }

    private void StopMove(AnimationState animationState)
    {
        if (animationState == AnimationState.Attack)
            _greenCommandMeleeUnitMove.StopMove();
    }
}
