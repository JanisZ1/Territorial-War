using Assets.CodeBase.Infrastructure.Services.Factory.Spawners;
using Assets.CodeBase.StaticData;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.AiUnitControll
{
    public class AiUnitSpawnControll : IAiUnitSpawnControll
    {
        private readonly ICoroutinerRunner _coroutinerRunner;
        private readonly IAiUnitSpawnerFactory _aiUnitSpawnerFactory;

        private float _spawnDelay = 3;

        public AiUnitSpawnControll(ICoroutinerRunner coroutinerRunner, IAiUnitSpawnerFactory aiUnitSpawnerFactory)
        {
            _coroutinerRunner = coroutinerRunner;
            _aiUnitSpawnerFactory = aiUnitSpawnerFactory;
        }

        public void StartSpawnTimer(CommandColor commandColor) =>
            _coroutinerRunner.StartCoroutine(SpawnWarriorDelay(commandColor));

        private IEnumerator SpawnWarriorDelay(CommandColor commandColor)
        {
            WaitForSeconds delay = new WaitForSeconds(_spawnDelay);

            while (true)
            {
                ChooseCommandSpawner(commandColor);
                yield return delay;
            }
        }

        private void ChooseCommandSpawner(CommandColor commandColor)
        {
            switch (commandColor)
            {
                case CommandColor.Green:
                    _aiUnitSpawnerFactory.UnitSpawner.Spawn(UnitType.GreenCommandMelee, _aiUnitSpawnerFactory.UnitSpawner.transform.position, Quaternion.identity);
                    break;

                case CommandColor.Red:
                    _aiUnitSpawnerFactory.UnitSpawner.Spawn(UnitType.RedCommandMelee, _aiUnitSpawnerFactory.UnitSpawner.transform.position, Quaternion.identity);
                    break;
            }
        }
    }
}
