using Assets.CodeBase.Data;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class UpperLineEdge : MonoBehaviour
    {
        [SerializeField] private LineRenderer _linerenderer;

        public Vector3 StartPosition { get; private set; }

        public Vector3 EndPosition { get; private set; }

        private readonly float _y = 10;

        public void SetUpperLineStartAndEndPosition(Vector2 focusPoint)
        {
            float halfOfDistanceToFocus = focusPoint.YDistance(to: ScanningLine.Directrix) / 2;
            float sqrDelta = SqrtDelta(focusPoint.y, halfOfDistanceToFocus);

            SetStartPosition(focusPoint.x, sqrDelta);
            SetEndPosition(focusPoint.x, sqrDelta);
        }

        private void SetStartPosition(float focusPointX, float sqrDelta)
        {
            Vector2 startPosition = new Vector2(focusPointX - sqrDelta, _y);

            Vector2 limitedPosition = BeachLine.LimitByBeachLineBoundaries(startPosition);

            StartPosition = new Vector3(limitedPosition.x, 0, limitedPosition.y);
        }

        private void SetEndPosition(float focusPointX, float sqrDelta)
        {
            Vector2 startPosition = new Vector2(focusPointX + sqrDelta, _y);

            Vector2 limitedPosition = BeachLine.LimitByBeachLineBoundaries(startPosition);

            EndPosition = new Vector3(limitedPosition.x, 0, limitedPosition.y);
        }

        private void Update()
        {
            _linerenderer.SetPosition(0, StartPosition);
            _linerenderer.SetPosition(1, EndPosition);
        }

        private float SqrtDelta(float focusPointY, float halfOfDistanceFromFocusToDirectrix)
        {
            //edge from X to X position calculation
            float delta = (_y - focusPointY + halfOfDistanceFromFocusToDirectrix) * 4 * halfOfDistanceFromFocusToDirectrix;

            if (delta < 0)
                return 0;

            return Mathf.Sqrt(delta);
        }
    }
}
