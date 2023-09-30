using UnityEngine;

[RequireComponent(typeof(GreenCommandUnitMove))]
public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private GreenCommandAnimator _greenCommandAnimator;
    [SerializeField] private TriggerObserver _triggerObserver;

    public bool IsFighting { get; private set; }

    private void SetAttackTrigger() =>
        _greenCommandAnimator.SetAttackTrigger();

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

    private void InitializeTarget(IDamageable damageable) =>
        _greenCommandAnimator.InitializeTarget(damageable);
}
