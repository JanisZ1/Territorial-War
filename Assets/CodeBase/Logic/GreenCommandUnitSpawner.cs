using Assets.CodeBase.Infrastructure.Services.Factory;
using Assets.CodeBase.StaticData;
using UnityEngine;

public class GreenCommandUnitSpawner : MonoBehaviour
{
    private GreenCommandUnit _previousUnit;
    private IGreenCommandUnitFactory _greenCommandUnitFactory;

    public void Construct(IGreenCommandUnitFactory warriorFactory) =>
        _greenCommandUnitFactory = warriorFactory;

    public void Spawn(UnitType unitType, Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = _greenCommandUnitFactory.CreateUnit(unitType, position, rotation);
        GreenCommandUnit unit = gameObject.GetComponent<GreenCommandUnit>();

        unit.PreviousUnit = _previousUnit;
        _previousUnit = unit;
    }
}
