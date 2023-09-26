namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public interface ILoadLevelState : IExitableState
    {
        void Enter(string scene);
    }
}