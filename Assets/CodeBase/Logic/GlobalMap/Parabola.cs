using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Parabola : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        private IEdgeFactory _edgeFactory;
        private UpperLineEdge _upperLineEdge;
        private ParabolaEdge _parabolaEdge;

        private bool _edgeCreated;
        private bool _parabolaEdgeCreated;

        private float _delta;
        private float _a;
        private float _b;

        public Vector2 FocusPoint { get; set; }

        public Vector2 Top { get; internal set; }

        public float DistanceToDirectrix { get; internal set; }

        public void Construct(IEdgeFactory edgeFactory) =>
            _edgeFactory = edgeFactory;

        public void InitializeUpperLineEdge(Vector2 directrix)
        {
            float stepCount = _lineRenderer.positionCount;

            if (!_edgeCreated)
            {
                _upperLineEdge = _edgeFactory.CreateUpperLineEdge();
                _edgeCreated = true;
            }

            SetUpperLineEdgeStartAndEndPosition(FocusPoint, DistanceToDirectrix);

            float fromX = _upperLineEdge.StartPosition.x;
            float toX = _upperLineEdge.EndPosition.x;

            float xStep = XStep(stepCount, fromX, toX);

            List<float> xPositions = UpdateXPositions(stepCount, fromX, xStep);
            Debug.Log("11111111111");
            for (int i = 0; i < xPositions.Count; i++)
            {
                float x = xPositions[i];
                float y = CalculateY(FocusPoint, directrix, x);

                Vector3 segmentPosition = new Vector3(x, 0, y);

                _lineRenderer.SetPosition(i, segmentPosition);
            }
        }

        public bool HasIntersectionPointsWith(Parabola otherParabola)
        {
            float distanceToDirectrix = DistanceToDirectrix;
            float otherParabolaDistanceToDirectrix = otherParabola.DistanceToDirectrix;

            float parabolaTopX = Top.x;
            float parabolaTopY = Top.y;

            float nextParabolaTopX = otherParabola.Top.x;
            float nextParabolaTopY = otherParabola.Top.y;

            float A = distanceToDirectrix - otherParabolaDistanceToDirectrix;

            float B = 2 * (otherParabolaDistanceToDirectrix * parabolaTopX
                - distanceToDirectrix * nextParabolaTopX);

            float C = distanceToDirectrix * nextParabolaTopX * nextParabolaTopX
                - otherParabolaDistanceToDirectrix * parabolaTopX * parabolaTopX + 4
                * (distanceToDirectrix * otherParabolaDistanceToDirectrix * nextParabolaTopY
                - distanceToDirectrix * otherParabolaDistanceToDirectrix * parabolaTopY);

            float delta = B * B - 4 * A * C;

            _a = A;
            _b = B;
            _delta = delta;

            if (delta < 0)
                return false;

            return true;
        }

        public void InitializeParabolaEdge()
        {
            if (!_parabolaEdgeCreated)
            {
                _parabolaEdge = _edgeFactory.CreateParabolaEdge();
                _parabolaEdgeCreated = true;
            }

            float sqrDelta = Mathf.Sqrt(_delta);
            float fromX = (-_b - sqrDelta) / (2 * _a);
            float toX = (-_b + sqrDelta) / (2 * _a);

            int stepCount = _lineRenderer.positionCount;

            float xStep = XStep(stepCount, fromX, toX);

            List<float> xPositions = UpdateXPositions(stepCount, fromX, xStep);
            for (int i = 0; i < xPositions.Count; i++)
            {
                float x = xPositions[i];
                float y = CalculateY(FocusPoint, ScanningLine.Directrix, x);

                Vector3 segmentPosition = new Vector3(x, 0, y);

                _lineRenderer.SetPosition(i, segmentPosition);
            }
            Debug.Log("2222222222");
        }

        private void SetUpperLineEdgeStartAndEndPosition(Vector2 focusPoint, float halfOfDistanceFromFocusToDirectrix)
        {
            float sqrDelta = _upperLineEdge.SqrtDelta(focusPoint.y, halfOfDistanceFromFocusToDirectrix);

            _upperLineEdge.SetStartPosition(focusPoint.x, sqrDelta);
            _upperLineEdge.SetEndPosition(focusPoint.x, sqrDelta);
        }

        private float CalculateY(Vector2 focusPoint, Vector2 directrix, float x) =>
            //parabola equation
            Mathf.Pow(x - focusPoint.x, 2) / (2 * (focusPoint.y - directrix.y)) + ((focusPoint.y + directrix.y) / 2);

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

        private float XStep(float stepCount, float fromX, float toX) =>
            Mathf.Abs(toX - fromX) / (stepCount - 1);
    }
}
