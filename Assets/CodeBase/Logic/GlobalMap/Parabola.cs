using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Parabola : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        public Vector3 ParabolaStart { get; set; }

        public Vector3 ParabolaEnd { get; set; }

        //intersection points with other parabola
        public Vector2 FirstIntersectionPoint { get; private set; }

        public Vector2 SecondIntersectionPoint { get; private set; }

        public UpperLineEdge UpperLineEdge { get; set; }

        //parabola edge that is drawing next(down) parabola
        public ParabolaEdge ParabolaEdge { get; set; }

        public Vector2 FocusPoint { get; private set; }

        public Vector2 NextParabolaFocusPoint { get; private set; }

        //Drawed parabola edges that is on this parabola
        public ParabolaEdge ToNextParabolaEdge { get; set; }

        public ParabolaEdge FromNextParabolaEdge { get; set; }

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
            //Gizmos.DrawSphere(new Vector3(FirstIntersectionPoint.x, 0, FirstIntersectionPoint.y), 0.5f);
            //Gizmos.DrawSphere(new Vector3(SecondIntersectionPoint.x, 0, SecondIntersectionPoint.y), 0.5f);
        }

        public void DrawParabola(Vector2 directrix, float fromX, float toX)
        {
            int stepCount = _lineRenderer.positionCount;

            float xStep = XStep(stepCount, fromX, toX);

            List<float> xPositions = UpdateXPositions(stepCount, fromX, xStep);

            for (int i = 0; i < xPositions.Count; i++)
            {
                float x = xPositions[i];
                float y = CalculateY(FocusPoint, directrix, x);

                Vector3 segmentPosition = new Vector3(x, 0, y);

                _lineRenderer.SetPosition(i, segmentPosition);
            }
        }

        public Vector2 UpdateIntersectionPointsWithNextParabola()
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

            //Get the intersection points from left to right
            if (firstX < secondX)
            {
                FirstIntersectionPoint = new Vector2(firstX, firstY);
                SecondIntersectionPoint = new Vector2(secondX, secondY);
                return new Vector2(firstX, firstY);
            }

            else
            {
                FirstIntersectionPoint = new Vector2(secondX, secondY);
                SecondIntersectionPoint = new Vector2(firstX, firstY);
                return new Vector2(secondX, secondY);
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

        public Parabola Copy()
        {
            GameObject copy = new GameObject(gameObject.name);
            LineRenderer lineRenderer = copy.AddComponent<LineRenderer>();

            Parabola parabola = copy.AddComponent<Parabola>();

            parabola._lineRenderer = lineRenderer;
            parabola._lineRenderer.positionCount = _lineRenderer.positionCount;
            parabola._lineRenderer.widthMultiplier = 0.1f;
            parabola.Construct(FocusPoint, NextParabolaFocusPoint);

            parabola.ParabolaStart = ParabolaStart;
            parabola.ParabolaEnd = ParabolaEnd;

            parabola.FirstIntersectionPoint = FirstIntersectionPoint;
            parabola.SecondIntersectionPoint = SecondIntersectionPoint;

            parabola.UpperLineEdge = UpperLineEdge;
            parabola.ParabolaEdge = ParabolaEdge;

            parabola.ToNextParabolaEdge = ToNextParabolaEdge;
            parabola.FromNextParabolaEdge = FromNextParabolaEdge;

            return parabola;
        }
    }
}
