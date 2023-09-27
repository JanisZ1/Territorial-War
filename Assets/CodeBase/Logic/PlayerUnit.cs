using Assets.CodeBase.Logic.Archer;
using Assets.CodeBase.Logic.Warrior;
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

    private void SetAttackTrigger() =>
        _animator.SetTrigger(AttackTriggerName);

    private void SetIdleTrigger() =>
        _animator.SetTrigger(IdleTriggerName);

    private void SetMoveTrigger() =>
        _animator.SetTrigger(MoveTriggerName);

    public virtual void Update()
    {
        //TODO: Refactor tomorrow all this method
        Vector3 direction = transform.right;
        float offset = 0.15f;
        Vector3 start = new Vector3(transform.position.x - offset, transform.position.y, transform.position.z);
        float maxDistance = 1f;
        if (Physics.Raycast(start, direction, out RaycastHit hit, maxDistance))
        {
            Collider hitCollider = hit.collider;

            if (hitCollider.GetComponentInParent<PlayerUnit>())
            {
                _unitMover.StopMove();
                SetIdleTrigger();
            }

            if (hitCollider.GetComponentInParent<EnemyUnit>() || hitCollider.GetComponentInParent<EnemyBase>())
            {
                SetAttackTrigger();
            }

            if (hitCollider.GetComponentInParent<EnemyUnit>())
            {
                InitializeTarget(hit);
            }
        }
        else
        {
            SetMoveTrigger();
            _unitMover.Move();
        }

        Debug.DrawRay(transform.position, direction, Color.green, maxDistance);
    }

    private void InitializeTarget(RaycastHit hit)
    {
        if (_archerAnimator)
            _archerAnimator.InitializeTarget(hit.collider);

        if (_warriorAnimator)
            _warriorAnimator.InitializeTarget(hit.collider);
    }
}
