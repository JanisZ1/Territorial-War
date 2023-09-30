using UnityEngine;

public class GreenCommandArcher : MonoBehaviour
{
    [SerializeField] private Transform _spawn;
    [SerializeField] private PlayerBullet _bullet;

    private EnemyUnit _enemyUnit;
    private float _shootingTime;

    private void Shoot() =>
        Instantiate(_bullet, _spawn.transform.position, _spawn.localRotation);

    private void Update()
    {
        //if (_closestEnemyCalculator != null)
        //    _enemyUnit = _closestEnemyCalculator.ClosestEnemy(to: transform);

        //if (_enemyUnit)
        //{
        //    if (_closestEnemyCalculator.Distance > 1 && _closestEnemyCalculator.Distance < 6)
        //    {
        //        _shootingTime += Time.deltaTime;
        //        if (_shootingTime > 1)
        //        {
        //            _shootingTime = 0;
        //            Shoot();
        //        }
        //    }
        //}
    }
}
