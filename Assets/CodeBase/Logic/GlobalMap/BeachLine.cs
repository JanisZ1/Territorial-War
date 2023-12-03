using System.Collections;
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
            SortParabolasFromLeftToRight();

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

                    if (parabola.FromNextParabolaEdge && parabola.ToNextParabolaEdge)
                    {
                        fromX = parabola.FromNextParabolaEdge.EndPosition.x;
                        toX = parabola.ToNextParabolaEdge.StartPosition.x;

                        if (toX < fromX)
                        {
                            _parabolas.Remove(parabola);
                            Destroy(parabola.gameObject);
                        }

                        parabola.DrawParabola(ScanningLine.Directrix, fromX, toX);
                        continue;
                    }

                    //first parabola dissecting by other parabola from right to left
                    if (parabola.FromNextParabolaEdge)
                    {
                        fromX = parabola.FromNextParabolaEdge.EndPosition.x;
                        toX = parabola.SecondIntersectionPoint.x;

                        if (toX < fromX)
                        {
                            _parabolas.Remove(parabola);
                            Destroy(parabola.gameObject);
                        }

                        parabola.DrawParabola(ScanningLine.Directrix, fromX, toX);
                        continue;
                    }
                    if (parabola.ToNextParabolaEdge)
                    {
                        fromX = parabola.FirstIntersectionPoint.x;
                        toX = parabola.ToNextParabolaEdge.StartPosition.x;

                        if (toX < fromX)
                        {
                            _parabolas.Remove(parabola);
                            Destroy(parabola.gameObject);
                        }

                        parabola.DrawParabola(ScanningLine.Directrix, fromX, toX);
                        continue;
                    }
                }

                if (parabola.UpperLineEdge)
                {
                    //parabola draw if its intersected second times
                    if (parabola.FromNextParabolaEdge && parabola.ToNextParabolaEdge)
                    {
                        float fromX = parabola.FromNextParabolaEdge.EndPosition.x;
                        float toX = parabola.ToNextParabolaEdge.StartPosition.x;

                        if (toX < fromX)
                        {
                            _parabolas.Remove(parabola);
                            Destroy(parabola.gameObject);
                        }

                        parabola.DrawParabola(ScanningLine.Directrix, fromX, toX);
                        continue;
                    }
                    if (parabola.ToNextParabolaEdge)
                    {
                        float fromX = parabola.UpperLineEdge.StartPosition.x;
                        float toX = parabola.ToNextParabolaEdge.StartPosition.x;

                        if (toX < fromX)
                        {
                            _parabolas.Remove(parabola);
                            Destroy(parabola.gameObject);
                        }

                        parabola.DrawParabola(ScanningLine.Directrix, fromX, toX);
                        continue;
                    }

                    if (parabola.FromNextParabolaEdge)
                    {
                        float fromX = parabola.FromNextParabolaEdge.EndPosition.x;
                        float toX = parabola.UpperLineEdge.EndPosition.x;

                        if (toX < fromX)
                        {
                            _parabolas.Remove(parabola);
                            Destroy(parabola.gameObject);
                        }

                        parabola.DrawParabola(ScanningLine.Directrix, fromX, toX);
                        continue;
                    }
                    else
                    {
                        float fromX = parabola.UpperLineEdge.StartPosition.x;
                        float tox = parabola.UpperLineEdge.EndPosition.x;

                        parabola.DrawParabola(ScanningLine.Directrix, fromX, tox);
                    }
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

            Destroy(secondParabola.FromNextParabolaEdge);
            Destroy(secondParabola.ToNextParabolaEdge);

            Destroy(secondParabola.gameObject);

            SortParabolasFromLeftToRight();
        }

        private void FindCircleEvents()
        {
            //Debug.Log("_parabolas count" + _parabolas.Count);
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

                //Debug.Log("firstParabola = " + firstParabola.FocusPoint);
                //Debug.Log("secondParabola = " + secondParabola.FocusPoint);
                //Debug.Log("thirdParabola = " + thirdParabola.FocusPoint);

                Vector2 firstParabolaFocus = firstParabola.FocusPoint;
                Vector2 secondParabolaFocus = secondParabola.FocusPoint;
                Vector2 thirdParabolaFocus = thirdParabola.FocusPoint;

                //slope coefficient between first and second focuses
                float k1 = (firstParabolaFocus.y - secondParabolaFocus.y) / (firstParabolaFocus.x - secondParabolaFocus.x);

                //slope coefficient between second and third focuses
                float k2 = (secondParabolaFocus.y - thirdParabolaFocus.y) / (secondParabolaFocus.x - thirdParabolaFocus.x);

                float perpendicularK1 = -1 / k1;
                float perpendicularK2 = -1 / k2;

                float xK1AndK2 = perpendicularK1 - perpendicularK2;

                //unknown y second addend
                float y3 = ab.y + perpendicularK1 * (-ab.x);

                // second equation unknown y second addend
                float y4 = bc.y + perpendicularK2 * (-bc.x);

                //calculating x by dividing by x expressed from y
                float x = (-y3 + y4) / xK1AndK2;
                float y = perpendicularK1 * x + y3;

                Vector2 middlePoint = new Vector2(x, y);

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

            Parabola intersectedParabolaCopy = intersectedParabola.Copy();

            //if parabola dissected first time
            if (!intersectedParabola.ToNextParabolaEdge)
                intersectedParabolaCopy.ToNextParabolaEdge = null;
            else
            {
                intersectedParabolaCopy.FromNextParabolaEdge = parabola.ParabolaEdge;
            }

            intersectedParabola.ToNextParabolaEdge = parabolaEdge;


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

        private void SortParabolasFromLeftToRight()
        {
            //TODO: Sorting by parabola secondIntersection point - fix the rightest parabola dont have that point
            //now sorting by parabola end of drawing points
            _parabolas.Sort((x1, x2) => x1.ParabolaEnd.x.CompareTo(x2.ParabolaEnd.x));
        }
    }
}

