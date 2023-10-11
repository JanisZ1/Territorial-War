using Assets.CodeBase.Logic.Spawners;

namespace Assets.CodeBase.Infrastructure.Services.AiUnitControll
{
    public interface IAiUnitSpawnControll : IService
    {
        void StartSpawnTimer(CommandColor green);
        void InitSpawners(GreenCommandUnitSpawner greenCommandUnitSpawner, RedCommandUnitSpawner redCommandUnitSpawner);
    }
}
