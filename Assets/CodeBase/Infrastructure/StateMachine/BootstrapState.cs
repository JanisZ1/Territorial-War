using Assets.CodeBase.Infrastructure.StateMachine;

namespace Assets.CodeBase.Infrastructure
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        private const string Bootstrap = "Bootstrap";
        private const string Main = "Main";

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;

            RegisterServices();
        }

        private void RegisterServices()
        {

        }

        public void Enter() =>
            _sceneLoader.Load(Bootstrap, EnterLoadLevel);

        private void EnterLoadLevel() =>
            _gameStateMachine.Enter<LoadLevelState>(Main);

        public void Exit()
        {
        }
    }
}