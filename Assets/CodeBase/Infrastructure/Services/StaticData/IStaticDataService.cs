using Assets.CodeBase.StaticData;

namespace Assets.CodeBase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        LevelStaticData ForLevel(string level);
        UnitStaticData ForUnit(UnitType unitType);
    }
}
