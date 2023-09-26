using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _bootstrapperPrefab;

        private void Awake()
        {
            GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper)
                return;

            Instantiate(_bootstrapperPrefab);
        }
    }
}