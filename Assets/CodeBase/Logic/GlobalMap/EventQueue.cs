using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class EventQueue : MonoBehaviour
    {
        public event Action<Vector2> SiteEventHappened;

        [SerializeField] private List<Vector2> _sites = new List<Vector2>();

        private void Update() =>
            CheckSiteEvents();

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
    }
}

