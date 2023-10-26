using System.Collections.Generic;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ParabolaObjectPool : IParabolaObjectPool
    {
        public List<Parabola> StoredParabolas { get; } = new List<Parabola>();
    }
}

