using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class BeachLine : MonoBehaviour
    {
        private EventQueue _eventQueue;
        private IParabolaFactory _parabolaFactory;

        private List<Parabola> _parabolas = new List<Parabola>();

        public void Construct(EventQueue eventQueue, IParabolaFactory parabolaFactory)
        {
            _eventQueue = eventQueue;
            _parabolaFactory = parabolaFactory;
        }

        private void Start() =>
            _eventQueue.SiteEventHappened += SiteEventHappened;

        private void OnDestroy() =>
            _eventQueue.SiteEventHappened -= SiteEventHappened;

        private void SiteEventHappened(Vector2 sitePosition) =>
            _parabolaFactory.CreateParabola(sitePosition);

        private void SortParabolasFromLeftToRight() =>
            _parabolas.Sort((x1, x2) => x1.FocusPoint.x.CompareTo(x2.FocusPoint.x));
    }
}

