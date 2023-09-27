using Assets.CodeBase.Logic.Archer;
using Assets.CodeBase.Logic.Warrior;
using System.Collections;
using System.Collections.Generic;
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
    private IGreenCommandSpawner _greenCommandSpawner;
    public int Id;

    private void Start()
    {
        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;
    }

    public void Construct(IGreenCommandSpawner greenCommandSpawner) =>
        _greenCommandSpawner = greenCommandSpawner;

    private void TriggerEnter(Collider obj)
    {
        Debug.Log("1111");
        if (obj.GetComponentInParent<PlayerUnit>() || obj.GetComponentInParent<PlayerBase>())
        {
            List<PlayerUnit> unitsSpawned = _greenCommandSpawner.UnitsSpawned;

            PlayerUnit playerUnit = GetUnitInFront(unitsSpawned);

            if (playerUnit != this)//FindedUnitInFront?
            {
                StopMoveCoroutine();
                _unitMover.StopMove();
                SetIdleTrigger();
            }
            else
            {
                Debug.Log("1111");
                _coroutine = StartCoroutine(MoveProcess());
            }
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

    private PlayerUnit GetUnitInFront(List<PlayerUnit> unitsSpawned)
    {
        PlayerUnit playerUnit = this;

        for (int i = 0; i < unitsSpawned.Count; i++)
        {
            if (Id > unitsSpawned[i].Id)
            {
                playerUnit = unitsSpawned[i];
            }
        }

        return playerUnit;
    }

    private void StopMoveCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void TriggerExit(Collider obj)
    {
        _greenCommandSpawner.UnitsSpawned.Remove(this);
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
