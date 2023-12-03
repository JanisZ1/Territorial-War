using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ParabolaEdge : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        public Vertex StartVertex
        {
            set
            {
                StartPosition = new Vector3(value.Position.x, 0, value.Position.y);
            }
        }

        public Vertex EndVertex;

        public Vector3 StartPosition { get; set; }

        public Vector3 EndPosition { get; set; }

        public void SetParabolaEdgeEndPosition(Vector2 rightEnd) =>
            SetEndPosition(rightEnd.x, rightEnd.y);

        private void SetEndPosition(float toX, float toY) =>
            EndPosition = new Vector3(toX, 0, toY);

        private void Update()
        {
            _lineRenderer.SetPosition(0, StartPosition);
            _lineRenderer.SetPosition(1, EndPosition);
        }
    }
}
