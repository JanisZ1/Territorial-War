using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ScanningLine : MonoBehaviour
    {
        public LineRenderer LineRenderer;

        private void Update() =>
            MoveForward();

        private void MoveForward() =>
            transform.position += Vector3.forward;
    }
}

