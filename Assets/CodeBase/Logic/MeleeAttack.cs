using Assets.CodeBase.Logic.Archer;
using Assets.CodeBase.Logic.Warrior;
using UnityEngine;

[RequireComponent(typeof(GreenCommandUnitMove))]
public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ArcherAnimator _archerAnimator;
    [SerializeField] private WarriorAnimator _warriorAnimator;
    [SerializeField] private GreenCommandUnitMove _greenCommandUnitMove;
    [SerializeField] private TriggerObserver _triggerObserver;

    private const string AttackTriggerName = "Attack";

    public bool IsFighting { get; private set; }

    public void SetAttackTrigger() =>
        _animator.SetTrigger(AttackTriggerName);

    private void Start()
    {
        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;
    }

    private void OnDestroy()
    {
        _triggerObserver.TriggerEnter -= TriggerEnter;
        _triggerObserver.TriggerExit -= TriggerExit;
    }

    private void TriggerEnter(Collider obj)
    {
        if (obj.GetComponentInParent<EnemyUnit>() || obj.GetComponentInParent<EnemyBase>())
        {
            IsFighting = true;
            SetAttackTrigger();
        }
        //TODO: Damageable to two command sides.
        if (obj.TryGetComponent(out IDamageable damageable))
        {
            IsFighting = true;
            InitializeTarget(damageable);
        }
    }

    private void TriggerExit(Collider obj)
    {
        if (obj.GetComponentInParent<EnemyUnit>() || obj.GetComponentInParent<EnemyBase>())
            IsFighting = false;

        if (obj.TryGetComponent(out IDamageable _))
            IsFighting = false;
    }

    private void InitializeTarget(IDamageable damageable)
    {
        if (_archerAnimator)
            _archerAnimator.InitializeTarget(damageable);

        if (_warriorAnimator)
            _warriorAnimator.InitializeTarget(damageable);
    }
}
