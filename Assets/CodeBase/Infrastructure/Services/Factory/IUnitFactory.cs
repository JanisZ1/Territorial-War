using Assets.CodeBase.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory
{
    public interface IUnitFactory : IService
    {
        GameObject CreateUnit(UnitType unitType, Vector3 at, Quaternion rotation);
    }
}
