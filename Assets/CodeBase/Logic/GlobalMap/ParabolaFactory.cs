using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ParabolaFactory : IParabolaFactory
    {
        private readonly IAssets _assets;
        private readonly IParabolaObjectPool _parabolaObjectPool;

        public ParabolaFactory(IAssets assets, IParabolaObjectPool parabolaObjectPool)
        {
            _assets = assets;
            _parabolaObjectPool = parabolaObjectPool;
        }

        public void CreateAndStoreParabolas(int sitesCount)
        {
            for (int i = 0; i < sitesCount; i++)
            {
                GameObject gameObject = _assets.Instantiate(AssetPath.ParabolaPath);

                Parabola parabola = gameObject.GetComponent<Parabola>();
                gameObject.SetActive(false);
                _parabolaObjectPool.StoredParabolas.Add(parabola);
            }
        }
    }
}

