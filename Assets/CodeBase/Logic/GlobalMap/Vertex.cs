using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Vertex
    {
        public readonly Vector2 Position;

        public Vertex(float x, float y) =>
            Position = new Vector2(x, y);
    }
}
