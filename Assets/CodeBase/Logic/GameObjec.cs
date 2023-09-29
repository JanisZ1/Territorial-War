using UnityEngine;

public class GameObjec : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
    }
}
