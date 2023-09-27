using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCommandUnitMove : MonoBehaviour
{
    private float _xVector = 1f;
    public Vector3 _playerUnitVector;

    private Coroutine _coroutine;
    [SerializeField] private Animator _animator;
    [SerializeField] private TriggerObserver _triggerObserver;
    private IGreenCommandSpawner _greenCommandSpawner;

    private const string MoveTriggerName = "Move";
    private const string IdleTriggerName = "Idle";

    public int Id { get; set; }

    public void Construct(IGreenCommandSpawner greenCommandSpawner) =>
        _greenCommandSpawner = greenCommandSpawner;

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
        if (obj.GetComponentInParent<GreenCommandUnitMove>() || obj.GetComponentInParent<PlayerBase>())
        {
            List<GreenCommandUnitMove> unitsSpawned = _greenCommandSpawner.UnitsSpawned;

            GreenCommandUnitMove playerUnit = GetUnitInFront(unitsSpawned);

            if (playerUnit != this)//FindedUnitInFront?
            {
                StopMove();
                StopMoveCoroutine();
                SetIdleTrigger();
            }
            else
            {
                StartCoroutine(MoveProcess());
            }
        }

        if (obj.GetComponentInParent<EnemyUnit>() || obj.GetComponentInParent<EnemyBase>())
        {
            StopMove();
            StopMoveCoroutine();
        }
    }

    private void TriggerExit(Collider obj)
    {
        _greenCommandSpawner.UnitsSpawned.Remove(this);
        SetMoveTrigger();
        _coroutine = StartCoroutine(MoveProcess());
    }

    public void Move()
    {
        _playerUnitVector = new Vector3(_xVector * Time.deltaTime, 0f, 0f);
        transform.Translate(_playerUnitVector);
    }

    public void StopMove() =>
        _playerUnitVector = Vector3.zero;

    public void StopMoveCoroutine()
    {
        if (_coroutine != null)
        {
            Debug.Log("Stop Coroutineeeeeeeeeeeee");
            StopCoroutine(_coroutine);
        }
    }

    public IEnumerator MoveProcess()
    {
        while (true)
        {
            yield return null;
            Move();
        }
    }

    private void SetMoveTrigger() =>
        _animator.SetTrigger(MoveTriggerName);


    private void SetIdleTrigger() =>
        _animator.SetTrigger(IdleTriggerName);

    private GreenCommandUnitMove GetUnitInFront(List<GreenCommandUnitMove> unitsSpawned)
    {
        GreenCommandUnitMove greenCommandUnitMove = this;

        for (int i = 0; i < unitsSpawned.Count; i++)
        {
            if (Id > unitsSpawned[i].Id)
            {
                greenCommandUnitMove = unitsSpawned[i];
            }
        }

        return greenCommandUnitMove;
    }
}
