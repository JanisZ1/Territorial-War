using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Edge : MonoBehaviour
    {
        [SerializeField] private LineRenderer _linerenderer;

        public Vector3 StartPosition { get; private set; }

        public Vector3 EndPosition { get; private set; }

        public float Y { get; private set; } = 10;

        public void SetStartPosition(float focusPointX, float sqrDelta) =>
            StartPosition = new Vector3(focusPointX - sqrDelta, 0, 10);

        public void SetEndPosition(float focusPointX, float sqrDelta) =>
            EndPosition = new Vector3(focusPointX + sqrDelta, 0, 10);

        private void Update()
        {
            _linerenderer.SetPosition(0, StartPosition);
            _linerenderer.SetPosition(1, EndPosition);
        }
    }
}
