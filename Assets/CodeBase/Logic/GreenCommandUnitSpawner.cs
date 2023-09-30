using Assets.CodeBase.Infrastructure.Services.Factory;
using UnityEngine;

public class GreenCommandUnitSpawner : MonoBehaviour
{
    private GreenCommandUnitMove _previousUnit;
    private IWarriorFactory _warriorFactory;
    private IArcherFactory _archerFactory;

    public void Construct(IWarriorFactory warriorFactory, IArcherFactory archerFactory)
    {
        _warriorFactory = warriorFactory;
        _archerFactory = archerFactory;
    }

    public GreenCommandUnitMove Spawn(GameObject _playerPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = _warriorFactory.CreateWarrior(_playerPrefab, position, rotation);
        GreenCommandUnitMove greenCommandUnitMove = gameObject.GetComponent<GreenCommandUnitMove>();

        greenCommandUnitMove.PreviousUnit = _previousUnit;

        _previousUnit = greenCommandUnitMove;
        return greenCommandUnitMove;
    }

    public void SpawnArcher(GameObject playerPrefab, Vector3 position, Quaternion rotation) =>
        _archerFactory.CreateArcher(playerPrefab, position, rotation);
}
