using Assets.CodeBase.Infrastructure.StateMachine;

namespace Assets.CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game() =>
            StateMachine = new GameStateMachine(new SceneLoader());
    }
}