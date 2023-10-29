using Assets.CodeBase.Infrastructure.Services.AssetProvider;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class EdgeFactory : IEdgeFactory
    {
        private readonly IAssets _assets;

        public EdgeFactory(IAssets assets) =>
            _assets = assets;

        public UpperLineEdge CreateUpperLineEdge() =>
            _assets.Instantiate(AssetPath.UpperLineEdgePath).GetComponent<UpperLineEdge>();

        public ParabolaEdge CreateParabolaEdge() =>
            _assets.Instantiate(AssetPath.ParabolaEdgePath).GetComponent<ParabolaEdge>();
    }
}

