using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Parabola : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        private IEdgeFactory _edgeFactory;
        private UpperLineEdge _upperLineEdge;
        private bool _edgeCreated;

        public float HalfOfDistanceFromFocusToDirectrix { get; private set; }

        public Vector2 FocusPoint { get; private set; }

        public void Construct(IEdgeFactory edgeFactory) =>
            _edgeFactory = edgeFactory;

        public void Initialize(Vector2 focusPoint, Vector2 directrix, float halfOfDistanceFromFocusToDirectrix)
        {
            FocusPoint = focusPoint;
            HalfOfDistanceFromFocusToDirectrix = halfOfDistanceFromFocusToDirectrix;

            float stepCount = _lineRenderer.positionCount;

            if (!_edgeCreated)
            {
                _upperLineEdge = _edgeFactory.CreateUpperLineEdge();
                _edgeCreated = true;
            }

            SetUpperLineEdgeStartAndEndPosition(focusPoint, halfOfDistanceFromFocusToDirectrix);

            float fromX = _upperLineEdge.StartPosition.x;
            float toX = _upperLineEdge.EndPosition.x;

            float xStep = XStep(stepCount, fromX, toX);

            List<float> xPositions = UpdateXPositions(stepCount, fromX, xStep);

            for (int i = 0; i < xPositions.Count; i++)
            {
                float x = xPositions[i];
                float y = CalculateY(focusPoint, directrix, x);

                Vector3 segmentPosition = new Vector3(x, 0, y);

                _lineRenderer.SetPosition(i, segmentPosition);
            }
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
