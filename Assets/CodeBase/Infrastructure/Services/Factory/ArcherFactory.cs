using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using Assets.CodeBase.Infrastructure.Services.Calculations;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory
{
    public class ArcherFactory : IArcherFactory
    {
        private readonly IAssets _assets;
        private readonly IClosestEnemyCalculator _closestEnemyCalculator;
        
        public ArcherFactory(IAssets assets, IClosestEnemyCalculator closestEnemyCalculator)
        {
            _assets = assets;
            _closestEnemyCalculator = closestEnemyCalculator;
        }

        public GameObject CreateArcher(GameObject prefab, Vector3 at, Quaternion rotation)
        {
            GameObject gameObject = Object.Instantiate(prefab, at, rotation);

            EnemyHealth enemyHealth = gameObject.GetComponentInChildren<EnemyHealth>();
            GreenCommandArcher playerArcher = gameObject.GetComponentInParent<GreenCommandArcher>();

            if (playerArcher)
                playerArcher.Construct(_closestEnemyCalculator);

            if (enemyHealth)
            {
                enemyHealth.Construct(_closestEnemyCalculator);

                _closestEnemyCalculator.Enemies.Add(gameObject.GetComponentInChildren<EnemyUnit>());
            }

            return gameObject;
        }
    }
}
