using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class EdgeFactory : IEdgeFactory
    {
        private readonly IAssets _assets;

        public EdgeFactory(IAssets assets) =>
            _assets = assets;

        public UpperLineEdge CreateUpperLineEdge() =>
            _assets.Instantiate(AssetPath.UpperLineEdgePath).GetComponent<UpperLineEdge>();

        public ParabolaEdge CreateParabolaEdge(Vertex vertex)
        {
            ParabolaEdge parabolaEdge = _assets.Instantiate(AssetPath.ParabolaEdgePath).GetComponent<ParabolaEdge>();

            Vector3 vertexPosition = new Vector3(vertex.Position.x, vertex.Position.y);

            parabolaEdge.StartPosition = vertexPosition;
            parabolaEdge.EndPosition = vertexPosition;

            return parabolaEdge;
        }
    }
}

