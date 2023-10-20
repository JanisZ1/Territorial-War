using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Parabola : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private ScanningLine _scanningLine;
        public Vector3 Focus;

        public void Construct(ScanningLine scanningLine) =>
            _scanningLine = scanningLine;

        private void Update()
        {
            MoveTransformToScanningLine();

            Vector2 topPoint = CalculateFocusPosition();
            InitializeFirstHalfOfParabola(topPoint);
        }

        private void MoveTransformToScanningLine() =>
            transform.position = Focus;

        private Vector2 CalculateFocusPosition()
        {
            Vector3 scanningLinePosition = _scanningLine.transform.position;

            Vector3 scanningLineleftCorner = _scanningLine.LineRenderer.GetPosition(0);
            Vector3 scanningLineRightCorner = _scanningLine.LineRenderer.GetPosition(1);

            Vector3 scanningLineMiddle = (scanningLineleftCorner + scanningLineRightCorner) * 0.5f;
            //sweeping Line Focus Position for test purposes
            Focus = new Vector3(scanningLineMiddle.x, scanningLinePosition.y, scanningLinePosition.z);

            Vector2 result = new Vector2(Focus.x, Focus.z);
            return result;
        }

        private void InitializeFirstHalfOfParabola(Vector2 focusPosition)
        {
            List<Vector3> segments = new List<Vector3>();
            float stepCount = _lineRenderer.positionCount;
            float step = focusPosition.y / stepCount;

            int firstParabolaindex = 0;
            for (int i = 0; i < (float)_lineRenderer.positionCount / 2; i++)
            {
                float x = step + ((i - ((float)_lineRenderer.positionCount / 2)) * step);
                float y = Mathf.Pow(x, 2);

                Vector3 segmentPosition = new Vector3(x, 0, y);

                _lineRenderer.SetPosition(firstParabolaindex, segmentPosition);
                segments.Add(segmentPosition);
                firstParabolaindex++;
            }

            CopyFirstHalfToSecondHalf(firstParabolaindex, segments);
        }

        private void CopyFirstHalfToSecondHalf(int firstParabolaindex, List<Vector3> segments)
        {
            int secondParabolaindex = firstParabolaindex - 1;

            for (int i = segments.Count - 1; i >= 0; i--)
            {
                Vector3 segmentPosition = new Vector3(segments[i].x, 0, segments[i].z);

                _lineRenderer.SetPosition(secondParabolaindex, new Vector3(-segmentPosition.x, 0, segmentPosition.z));
                secondParabolaindex++;
            }
        }
    }
}
