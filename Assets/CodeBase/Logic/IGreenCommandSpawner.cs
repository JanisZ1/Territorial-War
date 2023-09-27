using Assets.CodeBase.Infrastructure.Services;
using System.Collections.Generic;

public interface IGreenCommandSpawner : IService
{
    Dictionary<PlayerUnit, int> UnitsSpawned { get; }
    void AddToDictionary(PlayerUnit playerUnit);
}
