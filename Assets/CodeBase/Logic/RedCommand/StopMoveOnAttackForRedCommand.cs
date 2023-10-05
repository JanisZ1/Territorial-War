using UnityEngine;
using AnimationState = Assets.CodeBase.Logic.RedCommand.RedCommandAnimationState;

namespace Assets.CodeBase.Logic.RedCommand
{
    public class StopMoveOnAttackForRedCommand : MonoBehaviour
    {
        [SerializeField] private RedCommandMeleeUnitMove _redCommandMeleeUnitMove;
        [SerializeField] private RedCommandAnimator _redCommandAnimator;

        private void Start()
        {
            _redCommandAnimator.OnStateExited += StartMove;
            _redCommandAnimator.OnStateEntered += StopMove;
        }

        private void OnDestroy()
        {
            _redCommandAnimator.OnStateExited -= StartMove;
            _redCommandAnimator.OnStateEntered -= StopMove;
        }

        private void StartMove(AnimationState animationState)
        {
            if (animationState == AnimationState.Attack)
                _redCommandMeleeUnitMove.StartMove();
        }

        private void StopMove(AnimationState animationState)
        {
            if (animationState == AnimationState.Attack)
                _redCommandMeleeUnitMove.StopMove();
        }
    }
}
