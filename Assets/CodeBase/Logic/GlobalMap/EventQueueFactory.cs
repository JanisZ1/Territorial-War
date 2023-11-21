using Assets.CodeBase.Infrastructure.Services.AssetProvider;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class EventQueueFactory : IEventQueueFactory
    {
        private readonly IAssets _assets;

        public EventQueue EventQueue { get; private set; }

        public EventQueueFactory(IAssets assets) =>
            _assets = assets;

        public EventQueue CreateEventQueue()
        {
            EventQueue eventQueue = _assets.Instantiate(AssetPath.EventQueuePath).GetComponent<EventQueue>();

            EventQueue = eventQueue;

            return eventQueue;
        }
    }
}
