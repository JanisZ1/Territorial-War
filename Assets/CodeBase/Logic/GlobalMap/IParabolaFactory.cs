using Assets.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public interface IParabolaFactory : IService
    {
        GameObject CreateParabola(Vector2 focusPosition);
    }
}

