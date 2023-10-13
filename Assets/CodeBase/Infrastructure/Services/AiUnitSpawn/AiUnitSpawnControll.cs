using Assets.CodeBase.Infrastructure.Services.Factory.Spawners;
using Assets.CodeBase.StaticData;
using System;
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

        public void StartSpawnTimer(CommandColor commandColor)
        {
            UnitType unitType = ChooseUnitType(commandColor);
            _coroutinerRunner.StartCoroutine(SpawnUnitProcess(unitType));
        }

        private IEnumerator SpawnUnitProcess(UnitType unitType)
        {
            WaitForSeconds delay = new WaitForSeconds(_spawnDelay);

            while (true)
            {
                SpawnUnit(unitType);
                yield return delay;
            }
        }

        private void SpawnUnit(UnitType unitType) =>
            _aiUnitSpawnerFactory.UnitSpawner.Spawn(unitType, _aiUnitSpawnerFactory.UnitSpawner.transform.position, Quaternion.identity);

        private UnitType ChooseUnitType(CommandColor commandColor)
        {
            switch (commandColor)
            {
                case CommandColor.Green:
                    return UnitType.GreenCommandMelee;

                case CommandColor.Red:
                    return UnitType.RedCommandMelee;
            }

            return UnitType.GreenCommandMelee;
        }
    }
}
