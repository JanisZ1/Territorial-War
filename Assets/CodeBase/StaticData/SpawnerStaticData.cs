using System;
using UnityEngine;

namespace Assets.CodeBase.StaticData
{
    [Serializable]
    public class SpawnerStaticData
    {
        public Vector3 Position;

        public SpawnerStaticData(Vector3 position) =>
            Position = position;
    }
}
