namespace Assets.CodeBase.Logic.GreenCommand
{
    public interface IAnimationStateReader
    {
        void StateExited(int state);
        void StateEntered(int state);
        AnimationState State { get; }
    }
}