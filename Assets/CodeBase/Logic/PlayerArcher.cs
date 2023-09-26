using Assets.CodeBase.Infrastructure.Services.Calculations;
using UnityEngine;

public class PlayerArcher : PlayerUnit
{
    [SerializeField] private Transform _spawn;
    [SerializeField] private PlayerBullet _bullet;

    private EnemyUnit _enemyUnit;
    private float _shootingTime;
    private IClosestEnemyCalculator _closestEnemyCalculator;

    public void Construct(IClosestEnemyCalculator closestEnemyCalculator) =>
        _closestEnemyCalculator = closestEnemyCalculator;

    private void Shoot() =>
        Instantiate(_bullet, _spawn.transform.position, _spawn.localRotation);

    public override void Update()
    {
        base.Update();
        _enemyUnit = _closestEnemyCalculator.ClosestEnemy(to: transform);

        if (_enemyUnit)
        {
            if (_closestEnemyCalculator.Distance > 1 && _closestEnemyCalculator.Distance < 6)
            {
                _shootingTime += Time.deltaTime;
                if (_shootingTime > 1)
                {
                    _shootingTime = 0;
                    Shoot();
                }
            }
        }
    }
}
