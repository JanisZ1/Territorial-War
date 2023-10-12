using Assets.CodeBase.Logic.Spawners;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory.Spawners
{
    public interface ISpawnersFactory : IService
    {
        GameObject CreateCommandSpawner(CommandColor commandColor);
        GreenCommandUnitSpawner GreenCommandUnitSpawner { get; }
        RedCommandUnitSpawner RedCommandUnitSpawner { get; }
    }
}
