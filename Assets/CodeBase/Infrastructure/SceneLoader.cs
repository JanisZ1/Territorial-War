using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutinerRunner _coroutinerRunner;

        public SceneLoader(ICoroutinerRunner coroutinerRunner) =>
            _coroutinerRunner = coroutinerRunner;

        public void Load(string scene, Action onLoaded = null) =>
            _coroutinerRunner.StartCoroutine(LoadLevel(scene, onLoaded));

        private IEnumerator LoadLevel(string scene, Action onLoaded = null)
        {
            if (scene == SceneManager.GetActiveScene().name)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation progress = SceneManager.LoadSceneAsync(scene);

            while (!progress.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}