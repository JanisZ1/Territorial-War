using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class EventQueue : MonoBehaviour
    {
        public event Action<Vector2> SiteEventHappened;
        [SerializeField] private List<Vector2> _sites = new List<Vector2>();

        public event Action<float> CircleEventHappened;
        private List<float> _circleEvents;

        private void Update() =>
            CheckSiteEvents();

        public void AddToCircleEventList(float directrixYOnEventMoment) =>
            _circleEvents.Add(directrixYOnEventMoment);

        public void CheckCircleEvents()
        {
            for (int i = 0; i < _circleEvents.Count; i++)
            {
                if (ScanningLine.Directrix.y <= _circleEvents[i])
                {
                    InvokeCircleEventWithMaxY(i);
                }
            }
        }

        private void CheckSiteEvents()
        {
            for (int i = 0; i < _sites.Count; i++)
                if (ScanningLine.Directrix.y < _sites[i].y)
                    InvokeSiteEventWithMaxY(i);
        }

        private void InvokeSiteEventWithMaxY(int i)
        {
            SiteEventHappened?.Invoke(_sites[i]);
            _sites.Remove(_sites[i]);
        }

        private void InvokeCircleEventWithMaxY(int i)
        {
            CircleEventHappened?.Invoke(_circleEvents[i]);
            _circleEvents.Remove(_circleEvents[i]);
        }
    }
}

