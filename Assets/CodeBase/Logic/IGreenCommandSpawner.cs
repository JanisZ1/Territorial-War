using Assets.CodeBase.Infrastructure.Services;
using System.Collections.Generic;

public interface IGreenCommandSpawner : IService
{
    Queue<GreenCommandUnitMove> UnitsSpawnedQueue { get; }
    void Spawn(GreenCommandUnitMove playerUnit);
}
