using Assets.CodeBase.Logic.GreenCommand;
using Assets.CodeBase.Logic.RedCommand;
using UnityEngine;

[RequireComponent(typeof(GreenCommandMeleeUnitMove))]
public class GreenComandMeleeUnitFighting : MonoBehaviour
{
    [SerializeField] private GreenCommandAnimator _greenCommandAnimator;
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private GreenCommandEnemyUnitInFrontCalculator _greenCommandEnemyUnitInFrontCalculator;
    [SerializeField] private float _fightDistance;

    private IDamageable _damageable;
    private readonly int _damage = 1;

    private void Start()
    {
        _greenCommandAnimator.AttackEventFired += MakeDamageToEnemy;

        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;
    }

    private void OnDestroy()
    {
        _greenCommandAnimator.AttackEventFired -= MakeDamageToEnemy;

        _triggerObserver.TriggerEnter -= TriggerEnter;
        _triggerObserver.TriggerExit -= TriggerExit;
    }

    private void MakeDamageToEnemy() =>
        _damageable.TakeDamage(_damage);

    private void TriggerEnter(Collider obj)
    {
        (float distance, RedCommandUnit unit) closest = _greenCommandEnemyUnitInFrontCalculator.CalculateClosestEnemyForMelee();

        if (closest.distance < _fightDistance)
        {
            _damageable = closest.unit.GetComponentInChildren<IDamageable>();
            _greenCommandAnimator.SetAttackTrigger();
        }
    }

    private void TriggerExit(Collider obj)
    {
        (float distance, RedCommandUnit unit) closest = _greenCommandEnemyUnitInFrontCalculator.CalculateClosestEnemyForMelee();

        if (closest.distance < _fightDistance)
        {
            _greenCommandAnimator.SetIdleTrigger();
        }
    }
}
