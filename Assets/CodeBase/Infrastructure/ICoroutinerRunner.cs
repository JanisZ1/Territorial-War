using System.Collections;
using UnityEngine;

public interface ICoroutinerRunner
{
    Coroutine StartCoroutine(IEnumerator coroutine);
}
