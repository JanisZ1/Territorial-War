using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ScanningLine : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.2f;
        [SerializeField] private Vector3 _point;
        [SerializeField] private Parabola _parabola;

        public LineRenderer LineRenderer;

        private void Update() =>
            MoveForward();

        private void MoveForward()
        {
            transform.position += _speed * Time.deltaTime * Vector3.back;

            if (transform.position.z < _point.z)
            {
                Vector2 parabolaTop = CalculateParabolaTop();

                _parabola.Initialize(parabolaTop);
            }
        }

        private Vector2 CalculateParabolaTop()
        {
            Vector2 pointPosition = new Vector2(_point.x, _point.z);
            float halfOfDistanceToFocus = HalfOfDistanceToFocus(pointPosition);

            Vector2 parabolaTop = new Vector2(_point.x, _point.z - halfOfDistanceToFocus);
            return parabolaTop;
        }

        private float HalfOfDistanceToFocus(Vector2 pointPosition) =>
            Vector2.Distance(new Vector2(0, pointPosition.y), new Vector2(0, transform.position.z)) / 2;
    }
}

