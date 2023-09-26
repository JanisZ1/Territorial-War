using Assets.CodeBase.Infrastructure.Services.Calculations;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : EnemyUnit, IDamageable
{
    [SerializeField] private uint _unitHealth = 5;
    public UnityEvent _onUnitDied;
    public UnityEvent _onTakeDamage;
    private PlayerHealth _playerHealth;
    private IClosestEnemyCalculator _closestEnemyCalculator;

    public void Construct(IClosestEnemyCalculator closestEnemyCalculator) =>
        _closestEnemyCalculator = closestEnemyCalculator;

    public void TakeDamage(int damage)
    {
        _unitHealth -= (uint)damage;
        _onTakeDamage.Invoke();
        if (_unitHealth <= 0)
        {
            Die();
        }
    }

    public void MakeDamage(int damage) =>
        _playerHealth.TakeDamage(damage);

    private void Die()
    {
        if (transform.parent.gameObject != null)
        {
            Destroy(transform.parent.gameObject);
            _closestEnemyCalculator.Enemies.Remove(this);
            _onUnitDied.Invoke();
        }
    }

    public void FullPlayerHealthVariable(Collider other) =>
    _playerHealth = other.GetComponentInParent<PlayerHealth>();
}
