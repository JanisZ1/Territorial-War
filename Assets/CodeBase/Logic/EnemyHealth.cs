using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private uint _unitHealth = 5;
    public UnityEvent _onUnitDied;
    public UnityEvent _onTakeDamage;
    private PlayerHealth _playerHealth;

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
            _onUnitDied.Invoke();
        }
    }

    public void FullPlayerHealthVariable(Collider other) =>
        _playerHealth = other.GetComponent<PlayerHealth>();
}
