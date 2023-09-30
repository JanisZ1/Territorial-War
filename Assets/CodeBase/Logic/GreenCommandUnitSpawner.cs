using Assets.CodeBase.Infrastructure.Services.Factory;
using Assets.CodeBase.StaticData;
using UnityEngine;

public class GreenCommandUnitSpawner : MonoBehaviour
{
    private GreenCommandMeleeUnitMove _previousUnit;
    private IWarriorFactory _warriorFactory;
    private IArcherFactory _archerFactory;

    public UnitType UnitType;

    public void Construct(IWarriorFactory warriorFactory, IArcherFactory archerFactory)
    {
        _warriorFactory = warriorFactory;
        _archerFactory = archerFactory;
    }

    public GreenCommandMeleeUnitMove Spawn(UnitType unitType, Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = _warriorFactory.CreateWarrior(unitType, position, rotation);
        GreenCommandMeleeUnitMove greenCommandUnitMove = gameObject.GetComponent<GreenCommandMeleeUnitMove>();

        greenCommandUnitMove.PreviousUnit = _previousUnit;

        _previousUnit = greenCommandUnitMove;
        return greenCommandUnitMove;
    }

    public void SpawnArcher(GameObject playerPrefab, Vector3 position, Quaternion rotation) =>
        _archerFactory.CreateArcher(playerPrefab, position, rotation);
}
