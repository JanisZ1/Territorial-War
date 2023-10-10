using UnityEngine;

namespace Assets.CodeBase.Logic.RedCommand
{
    public class RedCommandMeleeUnitMove : RedCommandUnit
    {
        private const float MinimumDistance = 1.2f;
        private const float XVector = 1f;

        public bool MovingEnabled { get; private set; } = true;

        private void Update()
        {
            if (!MovingEnabled)
                return;

            if (!PreviousUnit)
            {
                Move();
                return;
            }

            if (DistanceToPreviousUnit() > MinimumDistance)
                Move();
        }

        private void Move()
        {
            Vector3 movingVector = new Vector3(-XVector * Time.deltaTime, 0f, 0f);
            transform.Translate(movingVector);
        }

        private float DistanceToPreviousUnit() =>
            Vector3.Distance(transform.position, PreviousUnit.transform.position);

        public void StartMove() =>
            MovingEnabled = true;

        public void StopMove() =>
            MovingEnabled = false;
    }
}