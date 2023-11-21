using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class BeachLine : MonoBehaviour
    {
        private EventQueue _eventQueue;

        public void Construct(EventQueue eventQueue) =>
            _eventQueue = eventQueue;

        private void Start() =>
            _eventQueue.SiteEventHappened += SiteEventHappened;

        private void OnDestroy() =>
            _eventQueue.SiteEventHappened -= SiteEventHappened;

        private void SiteEventHappened(Vector2 sitePosition)
        {

        }
    }
}

