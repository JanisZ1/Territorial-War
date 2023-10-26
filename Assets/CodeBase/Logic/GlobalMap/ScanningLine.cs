using Assets.CodeBase.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ScanningLine : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.2f;
        [SerializeField] private List<Vector3> _sites = new List<Vector3>();

        [SerializeField] private LineRenderer _lineRenderer;

        private IParabolaObjectPool _parabolaObjectPool;
        private Vector2 _directrix;

        public void Construct(IParabolaObjectPool parabolaObjectPool) => 
            _parabolaObjectPool = parabolaObjectPool;

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
            for (int i = 0; i < _sites.Count; i++)
            {
                Vector2 focusPoint = _sites[i].ConvertToVector2();

                if (_directrix.y < focusPoint.y)
                {
                    float halfOfDistanceToFocus = focusPoint.YDistance(to: _directrix) / 2;

                    Vector2 parabolaTop = CalculateParabolaTop(focusPoint, halfOfDistanceToFocus);

                    Parabola parabola = _parabolaObjectPool.StoredParabolas[i];
                    InitializeParabola(parabola, focusPoint, halfOfDistanceToFocus);
                }
            }
        }

        private void InitializeParabola(Parabola parabola, Vector2 focusPoint, float halfOfDistanceToFocus)
        {
            parabola.Initialize(focusPoint, _directrix, halfOfDistanceToFocus);
            parabola.gameObject.SetActive(true);
        }

        private Vector2 CalculateParabolaTop(Vector2 focusPoint, float halfOfDistanceToFocus)
        {
            Vector2 parabolaTop = new Vector2(focusPoint.x, focusPoint.y - halfOfDistanceToFocus);

            return parabolaTop;
        }
    }
}

