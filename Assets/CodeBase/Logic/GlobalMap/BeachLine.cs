using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class BeachLine : MonoBehaviour
    {
        [SerializeField] private EventQueue _eventQueue;

        private void Start() =>
            _eventQueue.SiteEventHappened += SiteEventHappened;

        private void OnDestroy() =>
            _eventQueue.SiteEventHappened -= SiteEventHappened;

        private void SiteEventHappened(Vector2 sitePosition)
        {

        }
    }
}

