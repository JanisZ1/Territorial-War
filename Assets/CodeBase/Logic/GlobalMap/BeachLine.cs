using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class BeachLine : MonoBehaviour
    {
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

        private void Start() =>
            _eventQueue.SiteEventHappened += SiteEventHappened;

        private void OnDestroy() =>
            _eventQueue.SiteEventHappened -= SiteEventHappened;

        private void Update()
        {
            UpdateParabolaIntersectionPoints();
            UpdateParabolaDrawing();
        }

        private void UpdateParabolaDrawing()
        {
            for (int i = 0; i < _parabolas.Count; i++)
            {
                Parabola parabola = _parabolas[i];

                if (parabola.ParabolaEdge)
                    parabola.InitializeParabolaEdge();

                if (parabola.UpperLineEdge)
                {
                    parabola.InitializeUpperLineEdge(ScanningLine.Directrix);
                }
            }
        }

        private void UpdateParabolaIntersectionPoints()
        {
            for (int i = 0; i < _parabolas.Count - 1; i++)
            {
                Parabola parabola = _parabolas[i];
                Parabola nextParabola = _parabolas[i + 1];

                if (parabola.ParabolaEdge && nextParabola.ParabolaEdge)
                {
                    nextParabola.FindIntersectionPointsWith(parabola);
                    continue;
                }

                if (parabola.ParabolaEdge)
                {
                    parabola.FindIntersectionPointsWith(nextParabola);
                }
                //also update the next to previous parabola intersection points
                if (nextParabola.ParabolaEdge)
                    nextParabola.FindIntersectionPointsWith(parabola);
            }
        }

        private void SiteEventHappened(Vector2 sitePosition)
        {
            Parabola intersectedParabola = FindArcIntersected(sitePosition);

            if (intersectedParabola)
            {
                Parabola parabola = _parabolaFactory.CreateParabola(sitePosition);
                ParabolaEdge parabolaEdge = _edgeFactory.CreateParabolaEdge();
                parabola.ParabolaEdge = parabolaEdge;
                _parabolas.Add(parabola);
            }
            else
            {
                Parabola parabola = _parabolaFactory.CreateParabola(sitePosition);
                UpperLineEdge upperLineEdge = _edgeFactory.CreateUpperLineEdge();

                parabola.UpperLineEdge = upperLineEdge;
                _parabolas.Add(parabola);
            }

            Debug.Log(intersectedParabola?.FocusPoint);

            SortParabolasFromLeftToRight();
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

