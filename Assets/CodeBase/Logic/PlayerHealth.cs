using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private uint _unitHealth = 5;
    public UnityEvent _onUnitDied;
    public UnityEvent _onTakeDamage;
    
    public void TakeDamage(int damage)
    {
        _unitHealth -= (uint)damage;
        _onTakeDamage.Invoke();

        if (_unitHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        _onUnitDied.Invoke();
    }
}
