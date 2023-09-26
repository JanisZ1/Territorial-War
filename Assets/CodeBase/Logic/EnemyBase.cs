using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private int _health = 10;
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
