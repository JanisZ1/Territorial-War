using Assets.CodeBase.Logic.Spawners;
using Assets.CodeBase.StaticData;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.AiUnitControll
{
    public class AiUnitSpawnControll : IAiUnitSpawnControll
    {
        private readonly ICoroutinerRunner _coroutinerRunner;
        private GreenCommandUnitSpawner _greenCommandUnitSpawner;
        private RedCommandUnitSpawner _redCommandUnitSpawner;
        private float _spawnDelay = 3;

        public AiUnitSpawnControll(ICoroutinerRunner coroutinerRunner) =>
            _coroutinerRunner = coroutinerRunner;

        public void InitSpawners(GreenCommandUnitSpawner greenCommandUnitSpawner, RedCommandUnitSpawner redCommandUnitSpawner)
        {
            _greenCommandUnitSpawner = greenCommandUnitSpawner;
            _redCommandUnitSpawner = redCommandUnitSpawner;
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
                    _greenCommandUnitSpawner.Spawn(UnitType.GreenCommandMelee, _greenCommandUnitSpawner.transform.position, Quaternion.identity);
                    break;

                case CommandColor.Red:
                    _redCommandUnitSpawner.Spawn(UnitType.RedCommandMelee, _redCommandUnitSpawner.transform.position, Quaternion.identity);
                    break;
            }
        }
    }
}
