using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Parabola : MonoBehaviour
    {
        public LineRenderer LineRenderer;

        public void Initialize(Vector2 parabolaTop, Vector2 focusPoint, Vector2 directrix) =>
            InitializeFirstHalfOfParabola(parabolaTop, focusPoint, directrix);

        private void InitializeFirstHalfOfParabola(Vector2 parabolaTop, Vector2 focusPoint, Vector2 directrix)
        {
            List<Vector3> segments = new List<Vector3>();
            float stepCount = LineRenderer.positionCount;

            float fromX = focusPoint.x + 0.4f;
            float toX = focusPoint.x - 0.4f;

            float xStep = Mathf.Abs(toX + fromX) / stepCount;

            List<float> xPositions = UpdateXPositions(stepCount, fromX, xStep);

            for (int i = 0; i < xPositions.Count; i++)
            {
                float x = xPositions[i];
                float y = Mathf.Pow(x - focusPoint.x, 2) / (2 * (focusPoint.y - directrix.y)) + ((focusPoint.y + directrix.y) / 2);

                Vector3 segmentPosition = new Vector3(x, 0, y);

                LineRenderer.SetPosition(i, segmentPosition);
                segments.Add(segmentPosition);
            }
        }

        private List<float> UpdateXPositions(float stepCount, float fromX, float xStep)
        {
            List<float> xPositions = new List<float>();

            for (float i = 0; i < stepCount; i++)
            {
                float x = fromX + i * xStep;
                xPositions.Add(x);
            }

            return xPositions;
        }
    }
}
