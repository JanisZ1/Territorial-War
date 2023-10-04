using UnityEngine;

namespace Assets.CodeBase.Logic.GreenCommand
{
    public abstract class GreenCommandAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public abstract void MakeDamageFromAnimation();

        public abstract void InitializeTarget(IDamageable damageable);

        private const string AttackTriggerName = "Attack";

        public const string IdleTriggerName = "Idle";

        public void SetAttackTrigger() =>
            _animator.SetTrigger(AttackTriggerName);

        public void SetIdleTrigger() =>
            _animator.SetTrigger(IdleTriggerName);
    }
}