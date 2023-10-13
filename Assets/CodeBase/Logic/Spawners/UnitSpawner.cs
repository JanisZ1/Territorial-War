using Assets.CodeBase.Infrastructure.Services.Factory.Unit;
using Assets.CodeBase.Infrastructure.Services.GreenCommandUnitsHandler;
using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Logic.Spawners
{
    public abstract class UnitSpawner : MonoBehaviour
    {
        public abstract void Spawn(UnitType unitType, Vector3 position, Quaternion rotation);
        public abstract void Construct(IUnitFactory unitFactory, IRedCommandUnitsHandler redCommandUnitsHandler, IGreenCommandUnitsHandler greenCommandUnitsHandler);
    }
}