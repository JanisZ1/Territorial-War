using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.StateMachine;

namespace Assets.CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutinerRunner coroutinerRunner) =>
            StateMachine = new GameStateMachine(new SceneLoader(coroutinerRunner), new AllServices());
    }
}