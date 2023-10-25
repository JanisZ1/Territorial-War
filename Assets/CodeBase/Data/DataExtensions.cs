using UnityEngine;

namespace Assets.CodeBase.Data
{
    public static class DataExtensions
    {
        public static Vector2 ConvertToVector2(this Vector3 vector3) =>
            new Vector2(vector3.x, vector3.z);

        public static float YDistance(this Vector2 from, Vector2 to)
        {
            from = new Vector2(0, from.y);
            to = new Vector2(0, to.y);

            return Vector2.Distance(from, to);
        }
    }
}
