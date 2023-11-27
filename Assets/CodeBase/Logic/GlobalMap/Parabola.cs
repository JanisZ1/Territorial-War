using Assets.CodeBase.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Parabola : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        private float _delta;

        public Vector2 FirstIntersectionPoint { get; private set; }
        private bool _edgeIsCreated;
        private float _firstX;
        private float _secondX;
        private float _firstY;
        private float _secondY;

        public UpperLineEdge UpperLineEdge { get; set; }

        public ParabolaEdge ParabolaEdge { get; set; }

        public Vector2 FocusPoint { get; set; }

        //Also the intersection point with other parabola
        public Vector2 RightEnd { get; set; }

        public LineRenderer LineRenderer => _lineRenderer;

        public void Construct(Vector2 focusPoint) =>
            FocusPoint = focusPoint;

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(new Vector3(_firstX, 0, _firstY), 0.5f);
            Gizmos.DrawSphere(new Vector3(_secondX, 0, _secondY), 0.5f);
        }

        public void InitializeUpperLineEdge(Vector2 directrix)
        {
            int stepCount = _lineRenderer.positionCount;

            float fromX = UpperLineEdge.StartPosition.x;

            float toX = UpperLineEdge.EndPosition.x;

            float xStep = XStep(stepCount, fromX, toX);

            SetUpperLineStartAndEndPosition(UpperLineEdge, FocusPoint);

            List<float> xPositions = UpdateXPositions(stepCount, fromX, xStep);

            for (int i = 0; i < xPositions.Count; i++)
            {
                float x = xPositions[i];
                float y = CalculateY(FocusPoint, directrix, x);

                Vector3 segmentPosition = new Vector3(x, 0, y);

                _lineRenderer.SetPosition(i, segmentPosition);
            }
        }

        public Vector2 FindRightIntersectionPointWith(Parabola otherParabola)
        {
            float b1md = FocusPoint.y - ScanningLine.Directrix.y;
            float b2md = otherParabola.FocusPoint.y - ScanningLine.Directrix.y;

            //solving the equation
            float a1 = 1 / (2 * b1md);
            float a2 = 1 / (2 * b2md);

            float b1 = a1 * (2 * FocusPoint.x);
            float b2 = a2 * (2 * otherParabola.FocusPoint.x);

            float c1 = (a1 * (FocusPoint.x * FocusPoint.x)) + (b1md / 2);
            float c2 = (a2 * (otherParabola.FocusPoint.x * otherParabola.FocusPoint.x)) + (b2md / 2);

            //diving one equation from other
            float a = a1 - a2;
            //Subtract b1 from b2 because otherwise the sign is not correct
            float b = b2 - b1;
            float c = c1 - c2;

            float discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
                return new Vector2(0, 0);

            _firstX = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
            _secondX = (-b - Mathf.Sqrt(discriminant)) / (2 * a);

            _firstY = CalculateY(FocusPoint, new Vector2(0, ScanningLine.Directrix.y), _firstX);
            _secondY = CalculateY(FocusPoint, new Vector2(0, ScanningLine.Directrix.y), _secondX);

            //Get the less y, that is on the beach line, other is behind
            if (_firstY < _secondY)
            {
                if (!_edgeIsCreated)
                {
                    FirstIntersectionPoint = new Vector2(_firstX, _firstY);
                    _edgeIsCreated = true;
                }
                return new Vector2(_firstX, _firstY);
            }

            else
            {
                if (!_edgeIsCreated)
                {
                    FirstIntersectionPoint = new Vector2(_secondX, _secondY);
                    _edgeIsCreated = true;
                }
                return new Vector2(_secondX, _secondY);
            }
        }

        public void InitializeParabolaEdge()
        {
            float fromX = FirstIntersectionPoint.x;
            float toX = _firstX;

            int stepCount = _lineRenderer.positionCount;

            float xStep = XStep(stepCount, fromX, toX);

            SetParabolaEdgeStartAndEndPosition(ParabolaEdge, FirstIntersectionPoint, RightEnd);

            List<float> xPositions = UpdateXPositions(stepCount, fromX, xStep);
            for (int i = 0; i < xPositions.Count; i++)
            {
                float x = xPositions[i];
                float y = CalculateY(FocusPoint, ScanningLine.Directrix, x);

                Vector3 segmentPosition = new Vector3(x, 0, y);

                _lineRenderer.SetPosition(i, segmentPosition);
            }
        }

        private void SetUpperLineStartAndEndPosition(UpperLineEdge upperLineEdge, Vector2 focusPoint)
        {
            float halfOfDistanceToFocus = FocusPoint.YDistance(to: ScanningLine.Directrix) / 2;
            float sqrDelta = upperLineEdge.SqrtDelta(focusPoint.y, halfOfDistanceToFocus);

            upperLineEdge.SetStartPosition(focusPoint.x, sqrDelta);
            upperLineEdge.SetEndPosition(focusPoint.x, sqrDelta);
        }

        private void SetParabolaEdgeStartAndEndPosition(ParabolaEdge parabolaEdge, Vector2 firstIntersectionPoint, Vector2 rightEnd)
        {
            parabolaEdge.SetStartPosition(firstIntersectionPoint.x, firstIntersectionPoint.y);
            parabolaEdge.SetEndPosition(rightEnd.x, rightEnd.y);
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
