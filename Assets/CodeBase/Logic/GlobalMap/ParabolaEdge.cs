using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ParabolaEdge : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        public Vector3 StartPosition { get; private set; }

        public Vector3 EndPosition { get; private set; }

        public void SetParabolaEdgeStartAndEndPosition(Vector2 firstIntersectionPoint, Vector2 rightEnd)
        {
            SetStartPosition(firstIntersectionPoint.x, firstIntersectionPoint.y);
            SetEndPosition(rightEnd.x, rightEnd.y);
        }

        private void SetStartPosition(float fromX, float fromY) =>
            StartPosition = new Vector3(fromX, 0, fromY);

        private void SetEndPosition(float toX, float toY) =>
            EndPosition = new Vector3(toX, 0, toY);

        private void Update()
        {
            _lineRenderer.SetPosition(0, StartPosition);
            _lineRenderer.SetPosition(1, EndPosition);
        }
    }
}
