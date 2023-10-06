using UnityEngine;

namespace Assets.CodeBase.Logic.GreenCommand
{
    public abstract class GreenCommandUnit : MonoBehaviour
    {
        public GreenCommandUnit PreviousUnit { get; set; }
    }
}