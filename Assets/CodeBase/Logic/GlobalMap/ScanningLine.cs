using Assets.CodeBase.Data;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ScanningLine : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.2f;

        [SerializeField] private LineRenderer _lineRenderer;

        public static Vector2 Directrix = new Vector2(0, 10);

        private void Update()
        {
            Directrix = transform.position.ConvertToVector2();

            MoveBack();
        }

        private void MoveBack() =>
            transform.position += _speed * Time.deltaTime * Vector3.back;
    }
}
