using Assets.CodeBase.Infrastructure.Services.Factory;
using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.Logic.RedCommand;
using Assets.CodeBase.StaticData;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Logic.Spawners
{
    public class RedCommandUnitSpawner : MonoBehaviour
    {
        private RedCommandUnit _previousUnit;
        [SerializeField] private Transform _spawnPosition;
        private IUnitFactory _greenCommandUnitFactory;
        private IRedCommandUnitsHandler _redCommandUnitsHandler;

        [SerializeField] private float _spawnDelay;

        public void Construct(IUnitFactory warriorFactory, IRedCommandUnitsHandler redCommandUnitsHandler, Infrastructure.Services.GreenCommandUnitsHandler.IGreenCommandUnitsHandler _greenCommandUnitsHandler)
        {
            _greenCommandUnitFactory = warriorFactory;
            _redCommandUnitsHandler = redCommandUnitsHandler;
        }

        private void Start() =>
            StartCoroutine(SpawnRedCommandWarrior());

        private IEnumerator SpawnRedCommandWarrior()
        {
            WaitForSeconds delay = new WaitForSeconds(_spawnDelay);
            while (true)
            {
                yield return delay;
                Spawn(UnitType.RedCommandMelee, _spawnPosition.position, Quaternion.identity);
            }
        }

        private void Spawn(UnitType unitType, Vector3 position, Quaternion rotation)
        {
            GameObject gameObject = _greenCommandUnitFactory.CreateUnit(unitType, position, rotation);
            RedCommandUnit unit = gameObject.GetComponent<RedCommandUnit>();

            unit.PreviousUnit = _previousUnit;
            _previousUnit = unit;

            _redCommandUnitsHandler.RedCommandUnits.Add(unit);
        }
    }
}