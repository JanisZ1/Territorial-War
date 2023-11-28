using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Parabola : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        public Vector3 ParabolaStart { get; private set; }

        public Vector3 ParabolaEnd { get; private set; }

        //intersection points with other parabola
        private Vector2 _firstIntersectionPoint;
        private Vector2 _secondIntersectionPoint;

        public UpperLineEdge UpperLineEdge { get; set; }

        public ParabolaEdge ParabolaEdge { get; set; }

        public Vector2 FocusPoint { get; set; }

        public Vector2 NextParabolaFocusPoint { get; set; }

        public void Construct(Vector2 focusPoint) =>
            FocusPoint = focusPoint;

        public void Construct(Vector2 focusPointPosition, Vector2 nextParabolaFocusPointPosition)
        {
            FocusPoint = focusPointPosition;
            NextParabolaFocusPoint = nextParabolaFocusPointPosition;
        }

        private void Update()
        {
            ParabolaStart = _lineRenderer.GetPosition(0);
            ParabolaEnd = _lineRenderer.GetPosition(40);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(new Vector3(_firstIntersectionPoint.x, 0, _firstIntersectionPoint.y), 0.5f);
            Gizmos.DrawSphere(new Vector3(_secondIntersectionPoint.x, 0, _secondIntersectionPoint.y), 0.5f);
        }

        public void DrawParabolaByUpperLineEdgeIntersection(Vector2 directrix)
        {
            int stepCount = _lineRenderer.positionCount;

            float fromX = UpperLineEdge.StartPosition.x;

            float toX = UpperLineEdge.EndPosition.x;

            float xStep = XStep(stepCount, fromX, toX);

            UpperLineEdge.SetUpperLineStartAndEndPosition(FocusPoint);

            List<float> xPositions = UpdateXPositions(stepCount, fromX, xStep);

            for (int i = 0; i < xPositions.Count; i++)
            {
                float x = xPositions[i];
                float y = CalculateY(FocusPoint, directrix, x);

                Vector3 segmentPosition = new Vector3(x, 0, y);

                _lineRenderer.SetPosition(i, segmentPosition);
            }
        }

        public Vector2 FindIntersectionPointsWithNextParabola()
        {
            float b1md = FocusPoint.y - ScanningLine.Directrix.y;
            float b2md = NextParabolaFocusPoint.y - ScanningLine.Directrix.y;

            //solving the equation
            float a1 = 1 / (2 * b1md);
            float a2 = 1 / (2 * b2md);

            float b1 = a1 * (2 * FocusPoint.x);
            float b2 = a2 * (2 * NextParabolaFocusPoint.x);

            float c1 = (a1 * (FocusPoint.x * FocusPoint.x)) + (b1md / 2);
            float c2 = (a2 * (NextParabolaFocusPoint.x * NextParabolaFocusPoint.x)) + (b2md / 2);

            //diving one equation from other
            float a = a1 - a2;
            //Subtract b1 from b2 because otherwise the sign is not correct
            float b = b2 - b1;
            float c = c1 - c2;

            float discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return new Vector2(0, 0);
            }

            float firstX = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
            float secondX = (-b - Mathf.Sqrt(discriminant)) / (2 * a);

            float firstY = CalculateY(FocusPoint, new Vector2(0, ScanningLine.Directrix.y), firstX);
            float secondY = CalculateY(FocusPoint, new Vector2(0, ScanningLine.Directrix.y), secondX);

            ParabolaEdge.SetParabolaEdgeStartAndEndPosition(_firstIntersectionPoint, _secondIntersectionPoint);
            //Get the intersection points from left to right
            if (firstX < secondX)
            {
                _firstIntersectionPoint = new Vector2(firstX, firstY);
                _secondIntersectionPoint = new Vector2(secondX, secondY);
                return new Vector2(firstX, firstY);
            }

            else
            {
                _firstIntersectionPoint = new Vector2(secondX, secondY);
                _secondIntersectionPoint = new Vector2(firstX, firstY);
                return new Vector2(secondX, secondY);
            }
        }

        public void DrawParabolaByOtherParabolaIntersection()
        {
            int stepCount = _lineRenderer.positionCount;

            float xStep = XStep(stepCount, fromX: _firstIntersectionPoint.x, toX: _secondIntersectionPoint.x);

            List<float> xPositions = UpdateXPositions(stepCount, fromX: _firstIntersectionPoint.x, xStep);
            for (int i = 0; i < xPositions.Count; i++)
            {
                float x = xPositions[i];
                float y = CalculateY(FocusPoint, ScanningLine.Directrix, x);

                Vector3 segmentPosition = new Vector3(x, 0, y);

                _lineRenderer.SetPosition(i, segmentPosition);
            }
        }

        private float CalculateY(Vector2 focusPoint, Vector2 directrix, float x) =>
            //parabola equation
            Mathf.Pow(x - focusPoint.x, 2) / (2 * (focusPoint.y - directrix.y)) + ((focusPoint.y + directrix.y) / 2);

        private List<float> UpdateXPositions(int stepCount, float fromX, float xStep)
        {
            List<float> xPositions = new List<float>();

            for (int i = 0; i < stepCount; i++)
            {
                float x = fromX + i * xStep;
                xPositions.Add(x);
            }

            return xPositions;
        }

        private float XStep(float stepCount, float fromX, float toX) =>
            Mathf.Abs(toX - fromX) / (stepCount - 1);
    }
}
