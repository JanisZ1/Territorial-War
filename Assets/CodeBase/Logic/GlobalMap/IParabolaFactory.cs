using Assets.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public interface IParabolaFactory : IService
    {
        Parabola CreateParabola(Vector2 focusPointPosition);
    }
}

