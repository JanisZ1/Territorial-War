using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Calculations
{
    public interface IClosestEnemyCalculator : IService
    {
        List<EnemyUnit> Enemies { get; }
        public float Distance { get; }
        EnemyUnit ClosestEnemy(Transform to);
    }
}
