using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Calculations
{
    public class ClosestEnemyCalculator : IClosestEnemyCalculator
    {
        public List<EnemyUnit> Enemies { get; } = new List<EnemyUnit>();

        public float Distance { get; private set; }

        public EnemyUnit ClosestEnemy(Transform to)
        {
            EnemyUnit enemyUnit = null;
            float minimumDistance = float.MaxValue;

            for (int i = 0; i < Enemies.Count; i++)
            {
                if (Enemies[i] == null)
                    continue;

                float distance = Vector3.Distance(to.position, Enemies[i].transform.position);

                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    Distance = minimumDistance;
                    enemyUnit = Enemies[i];
                }
            }

            return enemyUnit;
        }
    }
}
