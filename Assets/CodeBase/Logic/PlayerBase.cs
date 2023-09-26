using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private int _health = 10;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die() =>
        Destroy(gameObject);
}
