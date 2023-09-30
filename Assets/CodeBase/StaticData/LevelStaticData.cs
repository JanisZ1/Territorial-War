﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/LevelData")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelName;
        public List<SpawnerStaticData> Spawners;
    }
}
