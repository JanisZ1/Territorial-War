using UnityEngine;

namespace Assets.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "WindowData", menuName = "StaticData/WindowData")]
    public class WindowStaticData : ScriptableObject
    {
        public WindowType WindowType;
        public GameObject Prefab;
    }
}
