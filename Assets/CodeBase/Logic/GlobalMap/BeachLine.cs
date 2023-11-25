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
        private List<UpperLineEdge> _upperLineEdges = new List<UpperLineEdge>();

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
        }

        private void UpdateParabolaIntersectionPoints()
        {
            for (int i = 0; i < _parabolas.Count - 1; i++)
            {
                Parabola parabola = _parabolas[i];
                Parabola nextParabola = _parabolas[i + 1];

                Vector2 intersectionPoint = parabola.FindRightIntersectionPointWith(nextParabola);
                parabola.RightEnd = intersectionPoint;

                if (parabola.ParabolaEdge)
                    parabola.InitializeParabolaEdge();
            }
        }

        private void SiteEventHappened(Vector2 sitePosition)
        {
            FindArcIntersected(sitePosition);

            Parabola parabola = _parabolaFactory.CreateParabola(sitePosition);
            _parabolas.Add(parabola);
            SortParabolasFromLeftToRight();

            //TODO: Check parabola arc that is intersected with new parabola
            UpperLineEdge upperLineEdge = _edgeFactory.CreateUpperLineEdge();
            parabola.UpperLineEdge = upperLineEdge;

            
        }

        private Parabola FindArcIntersected(Vector2 newParabolaPosition)
        {
            Parabola parabolaIntersected = null;

            for (int i = 0; i < _parabolas.Count - 1; i++)
            {
                Parabola parabola = _parabolas[i];
                Parabola nextParabola = _parabolas[i + 1];

                if (newParabolaPosition.x < parabola.RightEnd.x && newParabolaPosition.x > nextParabola.RightEnd.x)
                {
                    ParabolaEdge parabolaEdge = _edgeFactory.CreateParabolaEdge();
                    nextParabola.ParabolaEdge = parabolaEdge;
                    return parabolaIntersected;
                }
            }

            return parabolaIntersected;
        }
        private void SortParabolasFromLeftToRight() =>
            _parabolas.Sort((x1, x2) => x1.FocusPoint.x.CompareTo(x2.FocusPoint.x));
    }
}

