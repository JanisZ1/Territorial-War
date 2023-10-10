using UnityEngine;

namespace Assets.CodeBase.Logic.GreenCommand
{
    public class GreenCommandMeleeUnitMove : GreenCommandUnit
    {
        private float _minimumDistance = 1.2f;
        private float _xVector = 1f;
        private bool _movingEnabled = true;

        private void Update()
        {
            if (!_movingEnabled)
                return;

            if (!PreviousUnit)
            {
                Move();
                return;
            }

            if (DistanceToPreviousUnit() > _minimumDistance)
                Move();
        }

        private void Move()
        {
            Vector3 movingVector = new Vector3(_xVector * Time.deltaTime, 0f, 0f);
            transform.Translate(movingVector);
        }

        private float DistanceToPreviousUnit() =>
            Vector3.Distance(transform.position, PreviousUnit.transform.position);

        public void StartMove() =>
            _movingEnabled = true;

        public void StopMove() =>
            _movingEnabled = false;
    }
}