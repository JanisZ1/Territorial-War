using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ScanningLine : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.2f;
        public LineRenderer LineRenderer;

        private void Update() =>
            MoveForward();

        private void MoveForward() =>
            transform.position += _speed * Time.deltaTime * Vector3.back;
    }
}

