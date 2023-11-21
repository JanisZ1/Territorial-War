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
        public static Vector2 Directrix = new Vector2(0, 10);
        private List<Parabola> _initializedParabolas = new List<Parabola>();

        public void Construct(IParabolaObjectPool parabolaObjectPool) =>
            _parabolaObjectPool = parabolaObjectPool;

        private void Update()
        {
            Directrix = transform.position.ConvertToVector2();

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

                if (Directrix.y < focusPoint.y)
                {
                    float halfOfDistanceToFocus = focusPoint.YDistance(to: Directrix) / 2;

                    Vector2 parabolaTop = CalculateParabolaTop(focusPoint, halfOfDistanceToFocus);

                    Parabola parabola = _parabolaObjectPool.StoredParabolas[i];
                    parabola.Top = parabolaTop;
                    parabola.DistanceToDirectrix = halfOfDistanceToFocus;
                    parabola.FocusPoint = focusPoint;

                    if (!_initializedParabolas.Contains(parabola))
                    {
                        _initializedParabolas.Add(parabola);
                        SortParabolasFromLeftToRight();
                    }

                    if (i < _initializedParabolas.Count - 1)
                    {
                        if (parabola.HasIntersectionPointsWith(_initializedParabolas[i + 1]))
                            parabola.InitializeParabolaEdge();
                        else
                            parabola.InitializeUpperLineEdge(Directrix);
                    }
                    else
                    {
                        parabola.InitializeUpperLineEdge(Directrix);
                    }
                    ActivateParabola(parabola);
                }
            }
        }

        private void SortParabolasFromLeftToRight() =>
            _initializedParabolas.Sort((x1, x2) => x1.FocusPoint.x.CompareTo(x2.FocusPoint.x));

        private void ActivateParabola(Parabola parabola) =>
            parabola.gameObject.SetActive(true);

        private Vector2 CalculateParabolaTop(Vector2 focusPoint, float halfOfDistanceToFocus)
        {
            Vector2 parabolaTop = new Vector2(focusPoint.x, focusPoint.y - halfOfDistanceToFocus);

            return parabolaTop;
        }
    }
}
