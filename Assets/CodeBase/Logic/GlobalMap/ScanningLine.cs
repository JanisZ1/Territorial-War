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
        private List<Parabola> _initializedParabolas = new List<Parabola>();

        public void Construct(IParabolaObjectPool parabolaObjectPool) =>
            _parabolaObjectPool = parabolaObjectPool;

        private void Update()
        {
            _directrix = transform.position.ConvertToVector2();

            MoveBack();
            ScanTerritory();
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

                    if (!_initializedParabolas.Contains(parabola))
                    {
                        _initializedParabolas.Add(parabola);
                        SortParabolasFromLeftToRight();
                        FindIntersections();
                    }
                }
            }
        }

        private void FindIntersections()
        {
            for (int i = 0; i < _initializedParabolas.Count - 1; i++)
            {
                Parabola parabola = _initializedParabolas[i];
                Parabola nextparabola = _initializedParabolas[i + 1];

                float firstParabolahalfOfDistanceFromFocusToDirectrix = parabola.HalfOfDistanceFromFocusToDirectrix;
                float nextParabolahalfOfDistanceFromFocusToDirectrix = nextparabola.HalfOfDistanceFromFocusToDirectrix;

                float parabolaTopX = parabola.FocusPoint.x;
                float parabolaTopY = parabola.FocusPoint.y - _directrix.y;

                float nextParabolaTopX = nextparabola.FocusPoint.x;
                float nextParabolaTopY = nextparabola.FocusPoint.y - _directrix.y;

                float A = firstParabolahalfOfDistanceFromFocusToDirectrix - nextParabolahalfOfDistanceFromFocusToDirectrix;

                float B = 2 * (nextParabolahalfOfDistanceFromFocusToDirectrix * parabolaTopX
                    - firstParabolahalfOfDistanceFromFocusToDirectrix * nextParabolaTopX);

                float C = firstParabolahalfOfDistanceFromFocusToDirectrix * nextParabolaTopX * nextParabolaTopX
                    - nextParabolahalfOfDistanceFromFocusToDirectrix * parabolaTopX * parabolaTopX + 4
                    * (firstParabolahalfOfDistanceFromFocusToDirectrix * nextParabolahalfOfDistanceFromFocusToDirectrix * nextParabolaTopY
                    - firstParabolahalfOfDistanceFromFocusToDirectrix * nextParabolahalfOfDistanceFromFocusToDirectrix * parabolaTopY);

                float delta = B * B - 4 * A * C;

                if (delta < 0)
                    return;

                float sqrDelta = Mathf.Sqrt(delta);

                float from = -B - sqrDelta / 2 / A;
                float to = -B + sqrDelta / 2 / A;
            }
        }

        private void SortParabolasFromLeftToRight() =>
            _initializedParabolas.Sort((x1, x2) => x1.FocusPoint.x.CompareTo(x2.FocusPoint.x));

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

