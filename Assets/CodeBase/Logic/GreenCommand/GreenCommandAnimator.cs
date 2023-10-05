using System;
using UnityEngine;

namespace Assets.CodeBase.Logic.GreenCommand
{
    public class GreenCommandAnimator : MonoBehaviour, IGreenCommandAnimationStateReader
    {
        [SerializeField] private Animator _animator;

        private readonly int _attackStateHash = Animator.StringToHash("Attack");
        private readonly int _idleStateHash = Animator.StringToHash("Idle");

        private const string AttackTriggerName = "Attack";

        public const string IdleTriggerName = "Idle";

        public GreenCommandAnimationState State { get; private set; }

        //Call event instead method
        public event Action<GreenCommandAnimationState> OnStateExited;
        public event Action<GreenCommandAnimationState> OnStateEntered;
        public event Action AttackEventFired;

        public void FireEventFromAnimation() =>
            AttackEventFired?.Invoke();

        public void PlayAttack() =>
            _animator.SetTrigger(AttackTriggerName);

        public void PlayIdle() =>
            _animator.SetTrigger(IdleTriggerName);

        public void StateExited(int state)
        {
            if (state == _attackStateHash)
                State = GreenCommandAnimationState.Attack;

            else if (state == _idleStateHash)
                State = GreenCommandAnimationState.Idle;

            OnStateExited?.Invoke(State);
        }

        public void StateEntered(int state)
        {
            if (state == _attackStateHash)
                State = GreenCommandAnimationState.Attack;

            else if (state == _idleStateHash)
                State = GreenCommandAnimationState.Idle;

            OnStateEntered?.Invoke(State);
        }
    }
}