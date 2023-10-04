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
        (float distance, RedCommandUnit unit) closest = _greenCommandEnemyUnitInFrontCalculator.CalculateClosestEnemyForMelee();

        if (closest.distance < _fightDistance)
        {
            _greenCommandAnimator.InitializeTarget(closest.unit.GetComponent<IDamageable>());
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
