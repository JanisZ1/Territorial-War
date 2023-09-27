using UnityEngine;

[RequireComponent(typeof(EnemyUnitMover))]
public class EnemyUnit : MonoBehaviour
{
    public IDamageable DamageableObject;
    [SerializeField] private Vector3Int _enemyPosition;
    private EnemyUnitState _currentEnemyUnitState;
    [SerializeField] private Animator _animator;
    private EnemyHealth _enemyHealth;
    private EnemyUnitMover _enemyUnitMover;

    public enum EnemyUnitState
    {
        Moving,
        InBattle,
        Idle
    }

    private void Awake()
    {
        _enemyUnitMover = GetComponentInParent<EnemyUnitMover>();
        _enemyHealth = GetComponentInChildren<EnemyHealth>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void SetAttackTrigger() =>
        _animator.SetTrigger("Attack");

    private void SetIdleTrigger() =>
        _animator.SetTrigger("Idle");

    private void Update()
    {
        Vector3 direction = transform.TransformDirection(-transform.right);
        float offset = 0.15f;
        Vector3 start = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
        RaycastHit hit;
        float maxDistance = 1f;

        if (Physics.Raycast(start, direction, out hit, maxDistance))
        {

            if (hit.collider.GetComponentInParent<PlayerUnit>())
            {
                SetState(EnemyUnitState.InBattle);
                _enemyHealth.FullPlayerHealthVariable(hit.collider);
                SetAttackTrigger();
                Debug.Log("Player unit is attacked");
            }
            if (hit.collider.GetComponentInParent<PlayerBase>())
            {
                SetState(EnemyUnitState.InBattle);
                FindObjectOfType<PlayerBase>().TakeDamage(1);
            }
            if (hit.collider.GetComponentInParent<EnemyUnit>())
            {
                SetState(EnemyUnitState.Idle);
            }
        }
        else
        {
            SetState(EnemyUnitState.Moving);
        }
        Debug.DrawRay(transform.position, direction, Color.green, maxDistance);
        if (_currentEnemyUnitState == EnemyUnitState.Moving)
        {
            SetIdleTrigger();
            _enemyUnitMover.Move();
            //место под SetMovingTrigger; для которого пока нету анимации
        }
        if (_currentEnemyUnitState == EnemyUnitState.Idle)
        {
            SetIdleTrigger();
        }
    }
    private void SetState(EnemyUnitState enemyUnitState)
    {
        _currentEnemyUnitState = enemyUnitState;
        if (_currentEnemyUnitState == EnemyUnitState.Idle)
        {
            _enemyUnitMover.StopMove();
        }
    }
}
