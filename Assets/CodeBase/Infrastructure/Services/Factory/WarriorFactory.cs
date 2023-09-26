﻿using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using Assets.CodeBase.Infrastructure.Services.Calculations;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory
{
    public class WarriorFactory : IWarriorFactory
    {
        private readonly IAssets _assets;
        private readonly IClosestEnemyCalculator _closestEnemyCalculator;

        public WarriorFactory(IAssets assets, IClosestEnemyCalculator closestEnemyCalculator)
        {
            _assets = assets;
            _closestEnemyCalculator = closestEnemyCalculator;
        }

        public GameObject CreateWarrior(GameObject prefab, Vector3 at, Quaternion rotation)
        {
            GameObject gameObject = Object.Instantiate(prefab, at, rotation);

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
