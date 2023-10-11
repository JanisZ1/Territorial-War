using Assets.CodeBase.Infrastructure.Services.StaticData;
using Assets.CodeBase.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory.Unit
{
    public class UnitFactory : IUnitFactory
    {
        private readonly IStaticDataService _staticData;

        public UnitFactory(IStaticDataService staticData) =>
            _staticData = staticData;

        public GameObject CreateUnit(UnitType unitType, Vector3 at, Quaternion rotation)
        {
            UnitStaticData unitStaticData = _staticData.ForUnit(unitType);

            GameObject gameObject = Object.Instantiate(unitStaticData.Prefab, at, rotation);

            return gameObject;
        }
    }
}
