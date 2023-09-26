using UnityEngine;

public class PlayerArcher : PlayerUnit
{
    [SerializeField] private Transform _spawn;
    [SerializeField] private PlayerBullet _bullet;
    private EnemyUnit[] _enemyUnits;
    private EnemyUnit _enemyUnit;
    private float _shootingTime;

    private void Start()
    {
        _enemyUnits = FindObjectsOfType<EnemyUnit>();
        _enemyUnit = ClosestEnemy();
    }

    private void Shoot() =>
        Instantiate(_bullet, _spawn.transform.position, _spawn.localRotation);

    private EnemyUnit ClosestEnemy()
    {
        EnemyUnit enemyUnit = null;
        float minimumDistance = float.MaxValue;

        for (int i = 0; i < _enemyUnits.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, _enemyUnits[i].transform.position);

            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                enemyUnit = _enemyUnits[i];
            }
        }

        return enemyUnit;
    }

    public override void Update()
    {
        base.Update();
        if (_enemyUnit)
        {
            float distance = Vector3.Distance(transform.position, _enemyUnit.transform.position);

            if (distance > 1 && distance < 6)
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
