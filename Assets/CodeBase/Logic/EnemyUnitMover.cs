using UnityEngine;

public class EnemyUnitMover : MonoBehaviour
{
    private Vector3 _enemyVector;

    public void Move()
    {
        _enemyVector = new Vector3(1f * Time.deltaTime, 0f, 0f);
        transform.Translate(_enemyVector);
    }

    public void StopMove() =>
        _enemyVector = Vector3.zero;
}
