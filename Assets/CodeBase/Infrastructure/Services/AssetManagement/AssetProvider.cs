using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.AssetProvider
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(string assetPath)
        {
            var prefab = Resources.Load<GameObject>(assetPath);

            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string assetPath, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(assetPath);

            return Object.Instantiate(prefab, parent);
        }

        public GameObject Instantiate(string assetPath, Vector3 at, Quaternion rotation)
        {
            var prefab = Resources.Load<GameObject>(assetPath);

            return Object.Instantiate(prefab, at, rotation);
        }
    }
}
