using Assets.CodeBase.Infrastructure.Services;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public interface IEdgeFactory : IService
    {
        Edge CreateEdge();
    }
}

