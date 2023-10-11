namespace Assets.CodeBase.Infrastructure.Services.Factory.Spawners
{
    public interface ISpawnersFactory : IService
    {
        void CreateCommandSpawner(CommandColor commandColor);
    }
}
