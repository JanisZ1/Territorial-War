using UnityEngine;

namespace Assets.CodeBase.Logic.GreenCommand
{
    public class GreenCommandMeleeUnitMove : GreenCommandUnit
    {
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
            Vector3 movingVector = new Vector3(XVector * Time.deltaTime, 0f, 0f);
            transform.Translate(movingVector);
        }

        private float DistanceToPreviousUnit() =>
            Vector3.Distance(transform.position, PreviousUnit.transform.position);
    }
}