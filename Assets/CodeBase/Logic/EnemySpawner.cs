using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private QueueWarrior _queueWarrior;
    [SerializeField] private float _spawnDelay;

    private void Start() =>
        StartCoroutine(SpawnEnemy());

    private IEnumerator SpawnEnemy()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnDelay);
        while (true)
        {
            yield return delay;
            _queueWarrior.AddedUnit();
        }
    }
}
