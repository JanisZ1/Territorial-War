using Assets.CodeBase.Infrastructure.Services;
using System.Collections.Generic;

public interface IGreenCommandSpawner : IService
{
    List<PlayerUnit> UnitsSpawned { get; }
    void Spawn(PlayerUnit playerUnit);
}
