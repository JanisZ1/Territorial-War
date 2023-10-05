using System;
using UnityEngine;
using AnimationState = Assets.CodeBase.Logic.RedCommand.RedCommandAnimationState;

namespace Assets.CodeBase.Logic.RedCommand
{
    public class RedCommandAnimator : MonoBehaviour, IRedCommandAnimationStateReader
    {
        [SerializeField] private Animator _animator;

        private readonly int _attackStateHash = Animator.StringToHash("Attack");
        private readonly int _idleStateHash = Animator.StringToHash("Idle");

        private const string AttackTriggerName = "Attack";

        public const string IdleTriggerName = "Idle";

        public AnimationState State { get; private set; }

        //Call event instead method
        public event Action<AnimationState> OnStateExited;
        public event Action<AnimationState> OnStateEntered;
        public event Action AttackEventFired;

        public void FireEventFromAnimation() =>
            AttackEventFired?.Invoke();

        public void PlayAttack() =>
            _animator.SetTrigger(AttackTriggerName);

        public void PlayIdle() =>
            _animator.SetTrigger(IdleTriggerName);

        public void StateExited(int state)
        {
            Debug.Log(state == _attackStateHash);
            if (state == _attackStateHash)
                State = AnimationState.Attack;

            else if (state == _idleStateHash)
                State = AnimationState.Idle;

            OnStateExited?.Invoke(State);
        }
        public void StateEntered(int state)
        {
            Debug.Log(state == _attackStateHash);
            if (state == _attackStateHash)
                State = AnimationState.Attack;

            else if (state == _idleStateHash)
                State = AnimationState.Idle;

            OnStateEntered?.Invoke(State);
        }
    }
}
