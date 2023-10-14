using UnityEngine;

namespace Assets.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "SpawnerData", menuName = "StaticData/SpawnerData")]
    public class SpawnerStaticData : ScriptableObject
    {
        public CommandColor CommandColor;
        public GameObject Prefab;
    }
}
