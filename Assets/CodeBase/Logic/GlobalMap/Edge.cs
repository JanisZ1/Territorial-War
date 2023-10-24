using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class Edge : MonoBehaviour
    {
        [SerializeField] private LineRenderer _linerenderer;

        public Vector3 StartPosition;
        public Vector3 EndPosition;

        private void Update()
        {
            _linerenderer.SetPosition(0, StartPosition);
            _linerenderer.SetPosition(1, EndPosition);
        }
    }
}
