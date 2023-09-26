using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutinerRunner
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();
            _game.StateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(gameObject);
        }
    }
}