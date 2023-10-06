using UnityEngine;

namespace Assets.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "StaticData/UnitData")]
    public class UnitStaticData : ScriptableObject
    {
        public UnitType UnitType;
        public GameObject Prefab;
    }
}
