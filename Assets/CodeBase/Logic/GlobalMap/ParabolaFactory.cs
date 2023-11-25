using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ParabolaFactory : IParabolaFactory
    {
        private readonly IAssets _assets;
        private readonly IEdgeFactory _edgeFactory;

        public ParabolaFactory(IAssets assets, IEdgeFactory edgeFactory)
        {
            _assets = assets;
            _edgeFactory = edgeFactory;
        }

        public Parabola CreateParabola(Vector2 focusPointPosition)
        {
            Parabola parabola = _assets.Instantiate(AssetPath.ParabolaPath).GetComponent<Parabola>();

            parabola.Construct(focusPointPosition);

            return parabola;
        }
    }
}

