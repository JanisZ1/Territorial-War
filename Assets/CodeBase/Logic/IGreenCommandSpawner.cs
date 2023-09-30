using Assets.CodeBase.Infrastructure.Services;

public interface IGreenCommandSpawner : IService
{
    void Spawn(GreenCommandUnitMove playerUnit);
}
