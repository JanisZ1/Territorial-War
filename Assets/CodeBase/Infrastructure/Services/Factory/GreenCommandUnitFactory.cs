using Assets.CodeBase.Infrastructure.Services.Calculations;
using Assets.CodeBase.Infrastructure.Services.StaticData;
using Assets.CodeBase.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory
{
    public class GreenCommandUnitFactory : IGreenCommandUnitFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly IClosestEnemyCalculator _closestEnemyCalculator;

        public GreenCommandUnitFactory(IStaticDataService staticData, IClosestEnemyCalculator closestEnemyCalculator)
        {
            _staticData = staticData;
            _closestEnemyCalculator = closestEnemyCalculator;
        }

        public GameObject CreateUnit(UnitType unitType, Vector3 at, Quaternion rotation)
        {
            UnitStaticData unitStaticData = _staticData.ForUnit(unitType);

            GameObject gameObject = Object.Instantiate(unitStaticData.Prefab, at, rotation);

            //TODO: Move enemy logic to other factory
            EnemyHealth enemyHealth = gameObject.GetComponentInChildren<EnemyHealth>();

            if (enemyHealth)
            {
                enemyHealth.Construct(_closestEnemyCalculator);
                _closestEnemyCalculator.Enemies.Add(gameObject.GetComponentInChildren<EnemyUnit>());
            }

            return gameObject;
        }
    }
}
