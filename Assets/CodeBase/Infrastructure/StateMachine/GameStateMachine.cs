namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private SceneLoader _sceneLoader;

        public GameStateMachine(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
    }
}