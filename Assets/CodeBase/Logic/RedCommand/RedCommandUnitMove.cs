using UnityEngine;

namespace Assets.CodeBase.Logic.RedCommand
{
    public class RedCommandUnitMove : RedCommandUnit
    {
        private Vector3 _enemyVector;

        private const float MinimumDistance = 1.2f;
        private const float XVector = 1f;

        private void Update()
        {
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
            _enemyVector = new Vector3(-XVector * Time.deltaTime, 0f, 0f);
            transform.Translate(_enemyVector);
        }

        private float DistanceToPreviousUnit() =>
            Vector3.Distance(transform.position, PreviousUnit.transform.position);

        public void StopMove() =>
            _enemyVector = Vector3.zero;
    }
}