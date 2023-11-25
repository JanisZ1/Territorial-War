using Assets.CodeBase.Infrastructure.Services.AssetProvider;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class BeachLineFactory : IBeachLineFactory
    {
        private readonly IAssets _assets;
        private readonly IEventQueueFactory _eventQueueFactory;
        private readonly IParabolaFactory _parabolaFactory;
        private readonly IEdgeFactory _edgeFactory;

        public BeachLineFactory(IAssets assets, IEventQueueFactory eventQueueFactory, IParabolaFactory parabolaFactory, IEdgeFactory edgeFactory)
        {
            _assets = assets;
            _eventQueueFactory = eventQueueFactory;
            _parabolaFactory = parabolaFactory;
            _edgeFactory = edgeFactory;
        }

        public void CreateBeachLine()
        {
            BeachLine beachLine = _assets.Instantiate(AssetPath.BeachLinePath).GetComponent<BeachLine>();
            beachLine.Construct(_eventQueueFactory.EventQueue, _parabolaFactory, _edgeFactory);
        }
    }
}

