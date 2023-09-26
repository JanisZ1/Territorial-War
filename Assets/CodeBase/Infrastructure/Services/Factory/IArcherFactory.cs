using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory
{
    public interface IArcherFactory : IService
    {
        GameObject CreateArcher(GameObject prefab, Vector3 at, Quaternion rotation);
    }
}
