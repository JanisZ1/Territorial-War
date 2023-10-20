using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Parabola : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private ScanningLine _scanningLine;

        public Vector2 Focus;
        private float _directrix;

        public void Construct(ScanningLine scanningLine) =>
            _scanningLine = scanningLine;

        public void CalculateTopPointPosition()
        {
            Vector3 scanningLinePosition = _scanningLine.transform.position;

        }
    }
}

