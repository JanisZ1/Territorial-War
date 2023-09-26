using System;

namespace Assets.CodeBase.Infrastructure.StateMachine
{
    public partial class GameStateMachine
    {
        public class LoadLevelState : IState
    {
            private GameStateMachine _gameStateMachine;
            private SceneLoader _sceneLoader;

            public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
            {
                _gameStateMachine = gameStateMachine;
                _sceneLoader = sceneLoader;
            }

            public void Enter()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
    }
}