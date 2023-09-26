using Assets.CodeBase.Logic.Archer;
using Assets.CodeBase.Logic.Warrior;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public IDamageable DamageableObject;
    private IMovable _iMovable;

    private PlayerUnitState _currentPlayerUnitState;
    [SerializeField] private Animator _animator;
    [SerializeField] private ArcherAnimator _archerAnimator;
    [SerializeField] private WarriorAnimator _warriorAnimator;

    private void Awake()
    {
        _iMovable = GetComponent<PlayerUnitMover>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void SetAttackTrigger() =>
        _animator.SetTrigger("Attack");

    private void SetIdleTrigger() =>
        _animator.SetTrigger("Idle");

    public enum PlayerUnitState
    {
        Moving,
        InBattle,
        Idle
    }

    public virtual void Update()
    {
        Vector3 direction = transform.right;
        float offset = 0.15f;
        Vector3 start = new Vector3(transform.position.x - offset, transform.position.y, transform.position.z);
        float maxDistance = 1f;
        if (Physics.Raycast(start, direction, out RaycastHit hit, maxDistance))
        {
            if (hit.collider.GetComponentInParent<EnemyUnit>())
            {
                SetState(PlayerUnitState.InBattle);

                if (_archerAnimator)
                    _archerAnimator.InitializeTarget(hit.collider);

                if (_warriorAnimator)
                    _warriorAnimator.InitializeTarget(hit.collider);

                SetAttackTrigger();
            }
            if (hit.collider.GetComponentInParent<EnemyBase>())
            {
                SetState(PlayerUnitState.InBattle);
                SetAttackTrigger();
            }
            if (hit.collider.GetComponentInParent<PlayerUnit>())
            {
                SetState(PlayerUnitState.Idle);
            }
        }
        else
        {
            SetState(PlayerUnitState.Moving);
        }

        Debug.DrawRay(transform.position, direction, Color.green, maxDistance);
        if (_currentPlayerUnitState == PlayerUnitState.Moving)
        {
            SetIdleTrigger();
            _iMovable.Move();
            //место под SetMovingTrigger; для которого пока нету анимации
        }
        if (_currentPlayerUnitState == PlayerUnitState.Idle)
        {
            SetIdleTrigger();
        }
    }

    private void SetState(PlayerUnitState playerUnitState)
    {
        _currentPlayerUnitState = playerUnitState;

        if (_currentPlayerUnitState == PlayerUnitState.Idle)
            _iMovable.StopMove();
    }
}
