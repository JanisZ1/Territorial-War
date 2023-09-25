using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStopTrigger : MonoBehaviour
{
    [SerializeField] private TestUnit _testUnit;
    [SerializeField] private List<TestUnit> _unitsInFront = new List<TestUnit>();
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out TestUnit otherUnit))
        {
            _unitsInFront.Add(otherUnit);
            _testUnit.UnitInMovementRange = GetUnitInFront();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out TestUnit otherUnit))
        {
            _unitsInFront.Remove(otherUnit);
            _testUnit.UnitInMovementRange = GetUnitInFront();
        }
            
    }
    public TestUnit GetUnitInFront()
    {
        TestUnit testUnit = _testUnit;
        for (int i = 0; i < _unitsInFront.Count; i++)
        {
            if (_unitsInFront[i].UnitID < testUnit.UnitID)
            {
                testUnit = _unitsInFront[i];
            }

        }
        if (testUnit != _testUnit)
            return testUnit;
        return null;
    }
}
