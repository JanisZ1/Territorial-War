using UnityEngine;
using UnityEngine.Events;

public class RedCommandUnitHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private uint _unitHealth = 5;
    public UnityEvent _onUnitDied;
    public UnityEvent _onTakeDamage;
    private GreenCommandUnitHealth _greenCommandUnitHealth;

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
        _greenCommandUnitHealth.TakeDamage(damage);

    private void Die()
    {
        if (transform.parent.gameObject != null)
        {
            Destroy(transform.parent.gameObject);
            _onUnitDied.Invoke();
        }
    }

    public void FullGreenCommandHealthVariable(Collider other) =>
    _greenCommandUnitHealth = other.GetComponentInParent<GreenCommandUnitHealth>();
}
