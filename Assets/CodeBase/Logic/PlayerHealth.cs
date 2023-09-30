using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _unitHealth = 5;

    public event Action OnUnitDied;
    public event Action OnTakeDamage;

    public void TakeDamage(int damage)
    {
        _unitHealth -= damage;
        OnTakeDamage.Invoke();

        if (_unitHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        OnUnitDied.Invoke();
    }
}
