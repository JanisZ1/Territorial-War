using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public interface ICoroutinerRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}