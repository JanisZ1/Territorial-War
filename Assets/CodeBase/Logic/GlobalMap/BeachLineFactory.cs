using Assets.CodeBase.Infrastructure.Services.AssetProvider;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class BeachLineFactory : IBeachLineFactory
    {
        private readonly IAssets _assets;
        private readonly IEventQueueFactory _eventQueueFactory;

        public BeachLineFactory(IAssets assets, IEventQueueFactory eventQueueFactory)
        {
            _assets = assets;
            _eventQueueFactory = eventQueueFactory;
        }

        public void CreateBeachLine()
        {
            BeachLine beachLine = _assets.Instantiate(AssetPath.BeachLinePath).GetComponent<BeachLine>();
            beachLine.Construct(_eventQueueFactory.EventQueue);
        }
    }
}

