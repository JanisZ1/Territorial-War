using Assets.CodeBase.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory
{
    public interface IWarriorFactory : IService
    {
        GameObject CreateWarrior(UnitType unitType, Vector3 at, Quaternion rotation);
    }
}
