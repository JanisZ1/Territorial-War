using Assets.CodeBase.Data;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ScanningLine : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.2f;
        [SerializeField] private Vector3 _point;
        [SerializeField] private Parabola _parabola;

        [SerializeField] private LineRenderer _lineRenderer;

        private Vector2 _directrix;
        private Vector2 _focus;

        private void Start() =>
            _focus = _point.ConvertToVector2();

        private void Update()
        {
            MoveBack();
            ScanTerritory();

            _directrix = transform.position.ConvertToVector2();
        }

        private void MoveBack() =>
            transform.position += _speed * Time.deltaTime * Vector3.back;

        private void ScanTerritory()
        {
            if (_directrix.y < _focus.y)
            {
                Vector2 focusPoint = _point.ConvertToVector2();
                float halfOfDistanceToFocus = focusPoint.YDistance(to: _directrix) / 2;

                Vector2 parabolaTop = CalculateParabolaTop(focusPoint, halfOfDistanceToFocus);

                _parabola.Initialize(focusPoint, _directrix, halfOfDistanceToFocus);
            }
        }

        private Vector2 CalculateParabolaTop(Vector2 focusPoint, float halfOfDistanceToFocus)
        {
            Vector2 parabolaTop = new Vector2(focusPoint.x, focusPoint.y - halfOfDistanceToFocus);

            return parabolaTop;
        }
    }
}

