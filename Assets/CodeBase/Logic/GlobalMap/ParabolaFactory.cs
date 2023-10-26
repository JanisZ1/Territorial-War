using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ParabolaFactory : IParabolaFactory
    {
        private readonly IAssets _assets;
        private readonly IParabolaObjectPool _parabolaObjectPool;
        private readonly IEdgeFactory _edgeFactory;

        public ParabolaFactory(IAssets assets, IParabolaObjectPool parabolaObjectPool, IEdgeFactory edgeFactory)
        {
            _assets = assets;
            _parabolaObjectPool = parabolaObjectPool;
            _edgeFactory = edgeFactory;
        }

        public void CreateAndStoreParabolas(int sitesCount)
        {
            for (int i = 0; i < sitesCount; i++)
            {
                GameObject gameObject = _assets.Instantiate(AssetPath.ParabolaPath);

                Parabola parabola = gameObject.GetComponent<Parabola>();
                parabola.Construct(_edgeFactory);
                gameObject.SetActive(false);
                _parabolaObjectPool.StoredParabolas.Add(parabola);
            }
        }
    }
}

