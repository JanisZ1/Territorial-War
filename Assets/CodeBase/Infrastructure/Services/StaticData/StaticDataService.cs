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
        private Dictionary<WindowType, WindowStaticData> _windows;
        private const string LevelsStaticDataPath = "StaticData/Levels";
        private const string UnitsStaticDataPath = "StaticData/Units";
        private const string SpawnersStaticDataPath = "StaticData/Spawners";
        private const string WindowsStaticDataPath = "StaticData/Windows";

        public void Load()
        {
            _levels = Resources.LoadAll<LevelStaticData>(LevelsStaticDataPath)
                .ToDictionary(x => x.LevelName, x => x);

            _units = Resources.LoadAll<UnitStaticData>(UnitsStaticDataPath)
                .ToDictionary(x => x.UnitType, x => x);

            _spawners = Resources.LoadAll<SpawnerStaticData>(SpawnersStaticDataPath)
                .ToDictionary(x => x.CommandColor, x => x);

            _windows = Resources.LoadAll<WindowStaticData>(WindowsStaticDataPath)
                .ToDictionary(x => x.WindowType, x => x);
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

        public WindowStaticData ForWindow(WindowType windowType) =>
            _windows.TryGetValue(windowType, out WindowStaticData windowData)
               ? windowData
               : null;
    }
}
