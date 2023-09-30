using UnityEngine;

public class GreenCommandUnitMove : Unit
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
