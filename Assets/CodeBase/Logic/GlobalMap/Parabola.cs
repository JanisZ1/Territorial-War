using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Parabola : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        public Vector3 Focus;

        private void Update() =>
            transform.position = Focus;

        public void InitializeFirstHalfOfParabola(Vector2 focusPosition)
        {
            Focus = new Vector3(focusPosition.x, 0, focusPosition.y);

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
