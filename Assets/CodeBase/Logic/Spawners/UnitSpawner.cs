using Assets.CodeBase.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Logic.Spawners
{
    public abstract class UnitSpawner : MonoBehaviour
    {
        public abstract void Spawn(UnitType unitType, Vector3 position, Quaternion rotation);
    }
}