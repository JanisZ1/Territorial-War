using Assets.CodeBase.Logic.Spawners;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory.Spawners
{
    public interface IHumanUnitSpawnerFactory : IService
    {
        GameObject CreateCommandSpawner(CommandColor commandColor);
        UnitSpawner UnitSpawner { get; }
    }
}
