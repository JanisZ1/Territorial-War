using System.Collections.Generic;
using UnityEngine;

public class GreenCommandUnitMove : MonoBehaviour, IGreenCommandUnit
{
    private float _xVector = 1f;
    public Vector3 _playerUnitVector;

    [SerializeField] private Animator _animator;
    [SerializeField] private TriggerObserver _triggerObserver;
    private IGreenCommandSpawner _greenCommandSpawner;

    private const string MoveTriggerName = "Move";
    private const string IdleTriggerName = "Idle";
    private bool _isMoving;

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
        IGreenCommandUnit greenCommandUnit = obj.GetComponentInParent<IGreenCommandUnit>();

        if (greenCommandUnit != null)
        {
            _greenCommandSpawner.UnitsSpawned.Add(greenCommandUnit);
            List<IGreenCommandUnit> unitsSpawned = _greenCommandSpawner.UnitsSpawned;
            IGreenCommandUnit playerUnit = GetUnitInFront(unitsSpawned);

            MoveOrStopMove(playerUnit);
        }

        if (obj.GetComponentInParent<EnemyUnit>() || obj.GetComponentInParent<EnemyBase>())
        {
            _isMoving = false;
        }
    }

    private void TriggerExit(Collider obj)
    {
        IGreenCommandUnit greenCommandUnitMove = obj.GetComponentInParent<IGreenCommandUnit>();

        if (greenCommandUnitMove != null || obj.GetComponentInParent<PlayerBase>())
        {
            _greenCommandSpawner.UnitsSpawned.Remove(greenCommandUnitMove);
            List<IGreenCommandUnit> unitsSpawned = _greenCommandSpawner.UnitsSpawned;

            IGreenCommandUnit playerUnit = GetUnitInFront(unitsSpawned);

            MoveOrStopMove(playerUnit);

            SetMoveTrigger();
        }
    }

    private void MoveOrStopMove(IGreenCommandUnit greenCommandUnitMove)
    {
        if (Id < greenCommandUnitMove.Id)//FindedUnitInFront?
        {
            _isMoving = false;
            SetIdleTrigger();
        }
        else
        {
            _isMoving = true;
        }
    }

    private void Update()
    {
        if (_isMoving)
            Move();
    }

    public void Move()
    {
        Vector3 movingVector = new Vector3(_xVector * Time.deltaTime, 0f, 0f);
        transform.Translate(movingVector);
    }

    private void SetMoveTrigger() =>
        _animator.SetTrigger(MoveTriggerName);


    private void SetIdleTrigger() =>
        _animator.SetTrigger(IdleTriggerName);

    private IGreenCommandUnit GetUnitInFront(List<IGreenCommandUnit> unitsSpawned)
    {
        IGreenCommandUnit greenCommandUnitMove = this;

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
