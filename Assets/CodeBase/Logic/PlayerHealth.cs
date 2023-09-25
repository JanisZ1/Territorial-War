using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private uint _unitHealth = 5;
    public UnityEvent _onUnitDied;
    public UnityEvent _onTakeDamage;
    [SerializeField] private EnemyHealth _enemyHealth;
    public void TakeDamage(int damage)
    {
        _unitHealth -= (uint)damage;
        _onTakeDamage.Invoke();
        if (_unitHealth <= 0)
        {
            Die();
        }
    }
    public void MakeDamage(int damage)
    {
        _enemyHealth.TakeDamage(damage);
    }
    private void Die()
    {
        Destroy(transform.parent.gameObject);
        _onUnitDied.Invoke();
    }
    public void FullEnemyHealthVariable(Collider other)
    {
        _enemyHealth = other.GetComponent<EnemyHealth>();
    }
}
