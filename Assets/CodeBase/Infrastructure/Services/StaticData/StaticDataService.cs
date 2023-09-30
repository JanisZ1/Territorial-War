using Assets.CodeBase.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<string, LevelStaticData> _levels;
        private const string LevelsStaticDataPath = "";

        public void Load()
        {
            _levels = Resources.LoadAll<LevelStaticData>(LevelsStaticDataPath)
                .ToDictionary(x => x.LevelName, x => x);
        }

        public LevelStaticData ForLevel(string level) =>
            _levels.TryGetValue(level, out LevelStaticData levelData)
                ? levelData
                : null;
    }
}
