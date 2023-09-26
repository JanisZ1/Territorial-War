namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public interface ILevelLoadState : IExitableState
    {
        void Enter(string scene);
    }
}