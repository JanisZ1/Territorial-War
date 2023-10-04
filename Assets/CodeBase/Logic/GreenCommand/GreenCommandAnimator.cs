using System;
using UnityEngine;

namespace Assets.CodeBase.Logic.GreenCommand
{
    public class GreenCommandAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private const string AttackTriggerName = "Attack";

        public const string IdleTriggerName = "Idle";

        //Call event instead method
        public event Action AttackEventFired;

        public void FireEventFromAnimation()
        {
            AttackEventFired?.Invoke();
        }

        public void SetAttackTrigger() =>
            _animator.SetTrigger(AttackTriggerName);

        public void SetIdleTrigger() =>
            _animator.SetTrigger(IdleTriggerName);
    }
}