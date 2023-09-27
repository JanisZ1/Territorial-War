using Assets.CodeBase.Infrastructure.Services;
using System.Collections.Generic;

public interface IGreenCommandSpawner : IService
{
    List<IGreenCommandUnit> UnitsSpawned { get; }
    void Spawn(GreenCommandUnitMove playerUnit);
}
