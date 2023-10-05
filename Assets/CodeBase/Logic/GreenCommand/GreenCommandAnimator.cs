using System;
using UnityEngine;

namespace Assets.CodeBase.Logic.GreenCommand
{
    public class GreenCommandAnimator : MonoBehaviour, IGreenCommandAnimationStateReader
    {
        [SerializeField] private Animator _animator;

        private readonly int _attackStateHash = Animator.StringToHash("Attack");
        private readonly int _rangeAttackStateHash = Animator.StringToHash("RangeAttack");
        private readonly int _idleStateHash = Animator.StringToHash("Idle");

        private const string AttackTriggerName = "Attack";
        private const string RangeAttackTriggerName = "RangeAttack";
        public const string IdleTriggerName = "Idle";

        public GreenCommandAnimationState State { get; private set; }

        //Call event instead method
        public event Action<GreenCommandAnimationState> OnStateExited;
        public event Action<GreenCommandAnimationState> OnStateEntered;
        public event Action AttackEventFired;
        public event Action RangeAttackEventFired;

        public void FireEventFromAnimation() =>
            AttackEventFired?.Invoke();

        public void FireEventFromAnimationForRange() =>
            RangeAttackEventFired?.Invoke();

        public void PlayAttack() =>
            _animator.SetTrigger(AttackTriggerName);

        public void PlayRangeAttack() =>
            _animator.SetTrigger(RangeAttackTriggerName);

        public void PlayIdle() =>
            _animator.SetTrigger(IdleTriggerName);

        public void StateExited(int state)
        {
            if (state == _attackStateHash)
                State = GreenCommandAnimationState.Attack;

            else if (state == _idleStateHash)
                State = GreenCommandAnimationState.Idle;

            else if (state == _rangeAttackStateHash)
                State = GreenCommandAnimationState.RangeAttack;

            OnStateExited?.Invoke(State);
        }

        public void StateEntered(int state)
        {
            if (state == _attackStateHash)
                State = GreenCommandAnimationState.Attack;

            else if (state == _idleStateHash)
                State = GreenCommandAnimationState.Idle;

            else if (state == _rangeAttackStateHash)
                State = GreenCommandAnimationState.RangeAttack;

            OnStateEntered?.Invoke(State);
        }
    }
}