using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory
{
    public class WarriorFactory : IWarriorFactory
    {
        private readonly IAssets _assets;

        public WarriorFactory(IAssets assets) =>
            _assets = assets;

        public GameObject CreateWarrior(GameObject prefab,Vector3 at, Quaternion rotation) =>
            Object.Instantiate(prefab, at, rotation);
    }
}
