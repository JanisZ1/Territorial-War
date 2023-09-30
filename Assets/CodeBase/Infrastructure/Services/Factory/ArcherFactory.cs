using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory
{
    public class ArcherFactory : IArcherFactory
    {
        private readonly IAssets _assets;

        public ArcherFactory(IAssets assets) =>
            _assets = assets;

        public GameObject CreateArcher(GameObject prefab, Vector3 at, Quaternion rotation)
        {
            GameObject gameObject = Object.Instantiate(prefab, at, rotation);

            return gameObject;
        }
    }
}
