using Assets.CodeBase.Logic.Spawners;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory.Spawners
{
    public interface IAiUnitSpawnerFactory : IService
    {
        UnitSpawner UnitSpawner { get; }
        GameObject CreateCommandSpawner(CommandColor commandColor);
    }
}
