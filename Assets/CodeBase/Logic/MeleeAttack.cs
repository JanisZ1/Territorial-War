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

    public void SetAttackTrigger() =>
        _animator.SetTrigger(AttackTriggerName);

    private void Start() =>
        _triggerObserver.TriggerEnter += TriggerEnter;

    private void OnDestroy() =>
        _triggerObserver.TriggerEnter -= TriggerEnter;

    private void TriggerEnter(Collider obj)
    {
        if (obj.GetComponentInParent<EnemyUnit>() || obj.GetComponentInParent<EnemyBase>())
            SetAttackTrigger();

        if (obj.GetComponentInParent<EnemyUnit>())
            InitializeTarget(obj);
    }

    private void InitializeTarget(Collider hitCollider)
    {
        if (_archerAnimator)
            _archerAnimator.InitializeTarget(hitCollider);

        if (_warriorAnimator)
            _warriorAnimator.InitializeTarget(hitCollider);
    }
}
