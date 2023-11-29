using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class BeachLine : MonoBehaviour
    {
        private static float _upperBoundary = 10;
        private static float _lowerBoundary = 0;
        private static float _leftBoundary = 0;
        private static float _rightBoundary = 10;

        private EventQueue _eventQueue;
        private IParabolaFactory _parabolaFactory;
        private IEdgeFactory _edgeFactory;

        private List<Parabola> _parabolas = new List<Parabola>();

        public void Construct(EventQueue eventQueue, IParabolaFactory parabolaFactory, IEdgeFactory edgeFactory)
        {
            _eventQueue = eventQueue;
            _parabolaFactory = parabolaFactory;
            _edgeFactory = edgeFactory;
        }

        private void Start()
        {
            _eventQueue.SiteEventHappened += SiteEventHappened;
            _eventQueue.CircleEventHappened += CircleEventHappened;
        }

        private void OnDestroy()
        {
            _eventQueue.SiteEventHappened -= SiteEventHappened;
            _eventQueue.CircleEventHappened -= CircleEventHappened;
        }

        private void Update()
        {
            UpdateParabolaIntersectionPoints();

            UpdateEdgePositions();

            UpdateParabolaDrawing();
        }

        private void UpdateParabolaIntersectionPoints()
        {
            for (int i = 0; i < _parabolas.Count; i++)
            {
                Parabola parabola = _parabolas[i];

                parabola.UpdateIntersectionPointsWithNextParabola();
            }
        }

        private void UpdateEdgePositions()
        {
            for (int i = 0; i < _parabolas.Count; i++)
            {
                Parabola parabola = _parabolas[i];

                if (parabola.ParabolaEdge)
                {
                    Vector2 firstIntersectionPoint = parabola.FirstIntersectionPoint;
                    Vector2 secondIntersectionPoint = parabola.SecondIntersectionPoint;

                    parabola.ParabolaEdge.SetParabolaEdgeStartAndEndPosition(firstIntersectionPoint, secondIntersectionPoint);
                }
                if (parabola.UpperLineEdge)
                {
                    parabola.UpperLineEdge.SetUpperLineStartAndEndPosition(parabola.FocusPoint);
                }
            }
        }

        public static Vector2 LimitByBeachLineBoundaries(Vector2 intersectionPoint)
        {
            float x = Mathf.Clamp(intersectionPoint.x, _leftBoundary, _rightBoundary);
            float y = Mathf.Clamp(intersectionPoint.y, _lowerBoundary, _upperBoundary);

            return new Vector2(x, y);
        }

        private void UpdateParabolaDrawing()
        {
            for (int i = 0; i < _parabolas.Count; i++)
            {
                Parabola parabola = _parabolas[i];

                if (parabola.ParabolaEdge)
                {
                    float fromX = parabola.FirstIntersectionPoint.x;
                    float toX = parabola.SecondIntersectionPoint.x;

                    parabola.DrawParabola(ScanningLine.Directrix, fromX, toX);
                }

                //first parabola dissecting by other parabola from left to right
                if (parabola.ToNextParabolaEdge)
                {
                    Debug.Log("ToNextParabolaEdge");
                    if (parabola.UpperLineEdge)
                    {
                        float fromX = parabola.UpperLineEdge.StartPosition.x;
                        float toX = parabola.ToNextParabolaEdge.StartPosition.x;
                        parabola.DrawParabola(ScanningLine.Directrix, fromX, toX);
                    }
                    continue;
                }

                //first parabola dissecting by other parabola from right to left
                if (parabola.FromNextParabolaEdge)
                {
                    float fromX = parabola.FromNextParabolaEdge.EndPosition.x;
                    float toX = parabola.ParabolaEnd.x;
                    parabola.DrawParabola(ScanningLine.Directrix, fromX, toX);
                    continue;
                }

                if (parabola.UpperLineEdge)
                {
                    float fromX = parabola.UpperLineEdge.StartPosition.x;
                    float tox = parabola.UpperLineEdge.EndPosition.x;

                    parabola.DrawParabola(ScanningLine.Directrix, fromX, tox);
                }
            }
        }

        private void SiteEventHappened(Vector2 sitePosition)
        {
            Parabola intersectedParabola = FindArcIntersected(sitePosition);

            if (intersectedParabola)
            {
                Parabola parabola = CreateIntersectedParabola(sitePosition, intersectedParabola);
                _parabolas.Add(parabola);
            }
            else
            {
                Parabola parabola = CreateUpperLineParabola(sitePosition);
                _parabolas.Add(parabola);
            }

            Debug.Log(intersectedParabola?.FocusPoint);

            SortParabolasFromLeftToRight();

            FindCircleEvents();
        }

        private void CircleEventHappened(float bottomPointOfCircle, Parabola secondParabola)
        {
            _parabolas.Remove(secondParabola);
            Destroy(secondParabola.gameObject);
            SortParabolasFromLeftToRight();
        }

        private void FindCircleEvents()
        {
            for (int i = 0; i < _parabolas.Count - 2; i++)
            {
                Parabola firstParabola = _parabolas[i];
                Parabola secondParabola = _parabolas[i + 1];
                Parabola thirdParabola = _parabolas[i + 2];

                //check to dont add to circle event list twice
                if (firstParabola.FocusPoint.x == secondParabola.FocusPoint.x
                    || secondParabola.FocusPoint.x == thirdParabola.FocusPoint.x
                    || firstParabola.FocusPoint.x == thirdParabola.FocusPoint.x)
                    continue;

                Vector2 ab = (firstParabola.FocusPoint + secondParabola.FocusPoint) / 2;
                Vector2 bc = (secondParabola.FocusPoint + thirdParabola.FocusPoint) / 2;
                Vector2 ca = (thirdParabola.FocusPoint + firstParabola.FocusPoint) / 2;

                Vector2 middlePoint = (ab + bc + ca) / 3;
                float radius = Vector2.Distance(middlePoint, firstParabola.FocusPoint);

                float circleEventPosition = middlePoint.y - radius;

                _eventQueue.AddToCircleEventList(circleEventPosition, secondParabola);
                Debug.Log(circleEventPosition);
            }
        }

        private Parabola CreateIntersectedParabola(Vector2 sitePosition, Parabola intersectedParabola)
        {
            Parabola parabola = _parabolaFactory.CreateParabola(sitePosition, intersectedParabola.FocusPoint);
            ParabolaEdge parabolaEdge = _edgeFactory.CreateParabolaEdge();

            intersectedParabola.ToNextParabolaEdge = parabolaEdge;

            Parabola intersectedParabolaCopy = intersectedParabola.Copy();
            intersectedParabolaCopy.ToNextParabolaEdge = null;

            intersectedParabolaCopy.FromNextParabolaEdge = parabolaEdge;
            _parabolas.Add(intersectedParabolaCopy);

            parabola.ParabolaEdge = parabolaEdge;
            return parabola;
        }

        private Parabola CreateUpperLineParabola(Vector2 sitePosition)
        {
            Parabola parabola = _parabolaFactory.CreateParabola(sitePosition);
            UpperLineEdge upperLineEdge = _edgeFactory.CreateUpperLineEdge();

            parabola.UpperLineEdge = upperLineEdge;
            return parabola;
        }

        private Parabola FindArcIntersected(Vector2 newParabolaPosition)
        {
            for (int i = 0; i < _parabolas.Count; i++)
            {
                Parabola parabola = _parabolas[i];

                if (newParabolaPosition.x > parabola.ParabolaStart.x && newParabolaPosition.x < parabola.ParabolaEnd.x)
                {
                    Debug.Log("parabola");
                    return parabola;
                }
            }
            return null;
        }

        private void SortParabolasFromLeftToRight() =>
            _parabolas.Sort((x1, x2) => x1.FocusPoint.x.CompareTo(x2.FocusPoint.x));
    }
}

