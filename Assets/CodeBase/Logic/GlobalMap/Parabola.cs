using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Parabola : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        public Vector2 Focus;
        private float _directrix;

        private void Start()
        {
        }
    }
}

