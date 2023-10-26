using Assets.CodeBase.Infrastructure.Services;
using System.Collections.Generic;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public interface IParabolaObjectPool : IService
    {
        List<Parabola> StoredParabolas { get; }
    }
}

