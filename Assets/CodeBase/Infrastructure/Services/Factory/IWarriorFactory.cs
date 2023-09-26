using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory
{
    public interface IWarriorFactory : IService
    {
        GameObject CreateWarrior(GameObject prefab, Vector3 at, Quaternion rotation);
    }
}
