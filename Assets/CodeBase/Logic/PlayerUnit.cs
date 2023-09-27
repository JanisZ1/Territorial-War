using Assets.CodeBase.Logic.Archer;
using Assets.CodeBase.Logic.Warrior;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UnitMover))]
public class PlayerUnit : MonoBehaviour
{
    private const string AttackTriggerName = "Attack";
    private const string IdleTriggerName = "Idle";
    private const string MoveTriggerName = "Move";

    [SerializeField] private Animator _animator;
    [SerializeField] private ArcherAnimator _archerAnimator;
    [SerializeField] private WarriorAnimator _warriorAnimator;
    [SerializeField] private UnitMover _unitMover;
    [SerializeField] private TriggerObserver _triggerObserver;
    private Coroutine _coroutine;
    protected int Index;

    private void Start()
    {
        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;
    }

    private void TriggerEnter(Collider obj)
    {
        if (obj.GetComponentInParent<PlayerUnit>())
        {
            StopMoveCoroutine();
            _unitMover.StopMove();
            SetIdleTrigger();
        }

        if (obj.GetComponentInParent<EnemyUnit>() || obj.GetComponentInParent<EnemyBase>())
        {
            StopMoveCoroutine();
            SetAttackTrigger();
        }

        if (obj.GetComponentInParent<EnemyUnit>())
        {
            StopMoveCoroutine();
            InitializeTarget(obj);
        }
    }

    private void StopMoveCoroutine() =>
        StopCoroutine(_coroutine);

    private void TriggerExit(Collider obj)
    {
        SetMoveTrigger();
        _coroutine = StartCoroutine(MoveProcess());
    }

    private IEnumerator MoveProcess()
    {
        yield return null;
        _unitMover.Move();
        _coroutine = StartCoroutine(MoveProcess());
    }

    private void SetAttackTrigger() =>
        _animator.SetTrigger(AttackTriggerName);

    private void SetIdleTrigger() =>
        _animator.SetTrigger(IdleTriggerName);

    private void SetMoveTrigger() =>
        _animator.SetTrigger(MoveTriggerName);

    private void InitializeTarget(Collider hitCollider)
    {
        if (_archerAnimator)
            _archerAnimator.InitializeTarget(hitCollider);

        if (_warriorAnimator)
            _warriorAnimator.InitializeTarget(hitCollider);
    }
}
