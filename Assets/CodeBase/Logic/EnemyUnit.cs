using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public IDamageable DamageableObject;
    [SerializeField] private Vector3Int _enemyPosition;
    private EnemyUnitState CurrentEnemyUnitState;
    [SerializeField] private Animator _animator;
    private EnemyHealth _enemyHealth;
    private IMovable _iMovable;
    public enum EnemyUnitState
    {
        Moving,
        InBattle,
        Idle
    }
    private void Awake()
    {
        _iMovable = GetComponent<EnemyUnitMover>();
        _enemyHealth = GetComponentInChildren<EnemyHealth>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void SetAttackTrigger()
    {
        _animator.SetTrigger("Attack");
    }
    private void SetIdleTrigger()
    {
        _animator.SetTrigger("Idle");
    }
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
        if (CurrentEnemyUnitState == EnemyUnitState.Moving)
        {
            SetIdleTrigger();
            _iMovable.Move();
            //место под SetMovingTrigger; для которого пока нету анимации
        }
        if (CurrentEnemyUnitState == EnemyUnitState.Idle)
        {
            SetIdleTrigger();
        }
    }
    private void SetState(EnemyUnitState enemyUnitState)
    {
        CurrentEnemyUnitState = enemyUnitState;
        if (CurrentEnemyUnitState == EnemyUnitState.Idle)
        {
            _iMovable.StopMove();
        }
    }
}
