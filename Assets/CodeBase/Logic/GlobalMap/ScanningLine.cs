using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ScanningLine : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.2f;
        [SerializeField] private Vector3 _point;
        [SerializeField] private Parabola _parabola;

        public LineRenderer LineRenderer;

        private void Update() =>
            MoveForward();

        private void MoveForward()
        {
            transform.position += _speed * Time.deltaTime * Vector3.back;

            if(transform.position.z < _point.z)
            {
                Vector2 focusPosition = new Vector2(_point.x, _point.z);
                _parabola.InitializeFirstHalfOfParabola(focusPosition);
            }
        }
    }
}

