using Assets.CodeBase.Infrastructure.Services.AssetProvider;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class EdgeFactory : IEdgeFactory
    {
        private readonly IAssets _assets;

        public EdgeFactory(IAssets assets) =>
            _assets = assets;
        public Edge CreateEdge() =>
            _assets.Instantiate(AssetPath.EdgePath).GetComponent<Edge>();
    }
}

