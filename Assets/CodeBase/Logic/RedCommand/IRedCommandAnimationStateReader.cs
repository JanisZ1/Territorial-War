using AnimationState = Assets.CodeBase.Logic.RedCommand.RedCommandAnimationState;

namespace Assets.CodeBase.Logic.RedCommand
{
    public interface IRedCommandAnimationStateReader
    {
        void StateExited(int state);
        void StateEntered(int state);
        AnimationState State { get; }
    }
}
