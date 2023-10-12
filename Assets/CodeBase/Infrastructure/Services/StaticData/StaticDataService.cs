using Assets.CodeBase.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<UnitType, UnitStaticData> _units;
        private Dictionary<CommandColor, SpawnerStaticData> _spawners;
        private const string LevelsStaticDataPath = "StaticData/Levels";
        private const string UnitsStaticDataPath = "StaticData/Units";
        private const string SpawnersStaticDataPath = "";

        public void Load()
        {
            _levels = Resources.LoadAll<LevelStaticData>(LevelsStaticDataPath)
                .ToDictionary(x => x.LevelName, x => x);

            _units = Resources.LoadAll<UnitStaticData>(UnitsStaticDataPath)
                .ToDictionary(x => x.UnitType, x => x);

            _spawners = Resources.LoadAll<SpawnerStaticData>(SpawnersStaticDataPath)
                .ToDictionary(x => x.CommandColor, x => x);
        }

        public LevelStaticData ForLevel(string level) =>
            _levels.TryGetValue(level, out LevelStaticData levelData)
                ? levelData
                : null;

        public UnitStaticData ForUnit(UnitType unitType) =>
            _units.TryGetValue(unitType, out UnitStaticData unitData)
               ? unitData
               : null;

        public SpawnerStaticData ForSpawner(CommandColor commandColor) =>
            _spawners.TryGetValue(commandColor, out SpawnerStaticData spawnerData)
               ? spawnerData
               : null;
    }
}
