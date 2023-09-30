using UnityEngine;

public abstract class GreenCommandAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public abstract void MakeDamageFromAnimation();

    public abstract void InitializeTarget(IDamageable damageable);

    private const string AttackTriggerName = "Attack";

    public void SetAttackTrigger() =>
        _animator.SetTrigger(AttackTriggerName);
}
