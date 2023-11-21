using Assets.CodeBase.Infrastructure.Services;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public interface IEventQueueFactory : IService
    {
        EventQueue CreateEventQueue();
        EventQueue EventQueue { get; }
    }
}