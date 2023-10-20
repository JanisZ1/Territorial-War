using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Parabola : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private ScanningLine _scanningLine;
        public Vector3 Focus;
        private float _directrix;
        private int _index;

        public void Construct(ScanningLine scanningLine) =>
            _scanningLine = scanningLine;
        private void Start()
        {
            _index = 9;
        }
        private void Update()
        {
            Vector2 topPoint = CalculateTopPointPosition();
            InitializeLineRendererPoints(topPoint);
        }

        private Vector2 CalculateTopPointPosition()
        {
            Vector3 scanningLineleftCorner = _scanningLine.LineRenderer.GetPosition(0);
            Vector3 scanningLineRightCorner = _scanningLine.LineRenderer.GetPosition(1);

            Vector3 scanningLineMiddle = scanningLineleftCorner + scanningLineRightCorner * 0.5f;

            Vector3 topPointPosition = Focus + scanningLineMiddle / 2;

            Vector2 result = new Vector2(topPointPosition.x, topPointPosition.z);
            return result;
        }

        private void InitializeLineRendererPoints(Vector2 topPointPosition)
        {
            List<Vector3> segments = new List<Vector3>();
            float stepCount = _lineRenderer.positionCount;
            float step = topPointPosition.x / stepCount;

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

            Copy(firstParabolaindex, segments);
        }

        private void Copy(int firstParabolaindex, List<Vector3> segments)
        {
            int secondParabolaindex = firstParabolaindex - 1;

            for (int i = segments.Count - 1; i >= 0; i--)
            {
                Debug.Log(secondParabolaindex);

                Vector3 segmentPosition = new Vector3(segments[i].x, 0, segments[i].z);

                _lineRenderer.SetPosition(secondParabolaindex, new Vector3(-segmentPosition.x, 0, segmentPosition.z));
                secondParabolaindex++;
            }
        }
    }
}
