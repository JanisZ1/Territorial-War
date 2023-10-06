namespace Assets.CodeBase.Logic.GreenCommand
{
    public interface IGreenCommandAnimationStateReader
    {
        void StateExited(int state);
        void StateEntered(int state);
        GreenCommandAnimationState State { get; }
    }
}