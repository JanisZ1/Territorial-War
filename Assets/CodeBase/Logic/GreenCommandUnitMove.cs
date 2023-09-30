using UnityEngine;

public class GreenCommandUnitMove : MonoBehaviour
{
    [SerializeField] private MeleeAttack _meleeAttack;

    private const float MinimumDistance = 1.2f;
    private const float XVector = 1f;

    public GreenCommandUnitMove PreviousUnit { get; set; }

    private void Update()
    {
        if (!PreviousUnit)
        {
            Move();
            return;
        }

        if (DistanceToPreviousUnit() > MinimumDistance && !PreviousUnitFighting())
            Move();
    }

    private void Move()
    {
        Vector3 movingVector = new Vector3(XVector * Time.deltaTime, 0f, 0f);
        transform.Translate(movingVector);
    }

    private bool PreviousUnitFighting() =>
        PreviousUnit._meleeAttack.IsFighting;

    private float DistanceToPreviousUnit() =>
        Vector3.Distance(transform.position, PreviousUnit.transform.position);
}
