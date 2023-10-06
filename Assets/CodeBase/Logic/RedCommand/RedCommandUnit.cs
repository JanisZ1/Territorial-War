using UnityEngine;

namespace Assets.CodeBase.Logic.RedCommand
{
    public abstract class RedCommandUnit : MonoBehaviour
    {
        public RedCommandUnit PreviousUnit { get; set; }
    }
}