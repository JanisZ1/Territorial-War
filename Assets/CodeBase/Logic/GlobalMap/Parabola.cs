using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Parabola : MonoBehaviour
    {
        public LineRenderer LineRenderer;
        [SerializeField] private Edge _edge;
        [SerializeField] private float _offset;

        public void InitializeParabola(Vector2 focusPoint, Vector2 directrix)
        {
            List<Vector3> segments = new List<Vector3>();
            float stepCount = LineRenderer.positionCount;
            float halfOfDistanceFromFocusToDirectrix = Vector2.Distance(new Vector2(0, focusPoint.y), new Vector2(0, directrix.y)) / 2;

            float delta = (_edge.Y - focusPoint.y + halfOfDistanceFromFocusToDirectrix) * 4 * halfOfDistanceFromFocusToDirectrix;

            if (delta < 0)
                return;

            float sqrDelta = Mathf.Sqrt(delta);

            _edge.SetStartPosition(focusPoint.x, sqrDelta);
            _edge.SetEndPosition(focusPoint.x, sqrDelta);

            float fromX = _edge.StartPosition.x;
            float toX = _edge.EndPosition.x;
            float xStep = XStep(stepCount, fromX, toX);

            List<float> xPositions = UpdateXPositions(stepCount, fromX, xStep);

            for (int i = 0; i < xPositions.Count; i++)
            {
                float x = xPositions[i];

                //parabola equation
                float y = Mathf.Pow(x - focusPoint.x, 2) / (2 * (focusPoint.y - directrix.y)) + ((focusPoint.y + directrix.y) / 2);

                Vector3 segmentPosition = new Vector3(x, 0, y);

                LineRenderer.SetPosition(i, segmentPosition);
                segments.Add(segmentPosition);
            }
        }

        private float XStep(float stepCount, float fromX, float toX) =>
            Mathf.Abs(toX - fromX) / (stepCount - 1);

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
