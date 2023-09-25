using System.Collections.Generic;
using UnityEngine;
public class TestUnit : MonoBehaviour
{
    public bool IsMoving;
    public int UnitID { get; private set; }
    public TestUnit UnitInMovementRange { get; set; }

    private void Update()
    {

        if (UnitInMovementRange == null)
        {
            IsMoving = true;
        }
        if (UnitInMovementRange)
        {
            IsMoving = false;
        }
        if (IsMoving)
        {
            transform.Translate(Vector3.forward * 1 * Time.deltaTime);
        }

    }
    public void Initialize(int unitID)
    {
        UnitID = unitID;
    }
    public void Move()
    {
        IsMoving = true;
    }
    public void StopMove()
    {
        IsMoving = false;
    }

}
