using UnityEngine;

public class PlayerArcher : PlayerUnit
{
    [SerializeField] private Transform _spawn;
    [SerializeField] private PlayerBullet _bullet;
    private EnemyUnit _enemyUnit;
    private float _shootingTime;

    private void Start() =>
        _enemyUnit = FindObjectOfType<EnemyUnit>();

    private void Shoot()
    {
        if (_enemyUnit)
            Instantiate(_bullet, _spawn.transform.position, _spawn.localRotation);
    }

    public override void Update()
    {
        //неправильная строчка - надо чтобы переменная заполнялась при определенной дистанции 
        _enemyUnit = FindObjectOfType<EnemyUnit>();
        base.Update();
        if (_enemyUnit)
        {
            float distance = Vector3.Distance(transform.position, _enemyUnit.transform.position);
            //Debug.Log(distance);
            if (distance < 3)
            {
                return;
            }
            if (distance < 6)
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
