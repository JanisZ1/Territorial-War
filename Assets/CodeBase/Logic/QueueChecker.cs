using UnityEngine;

public class QueueChecker : MonoBehaviour
{
    private IGreenCommandSpawner _greenCommandSpawner;
    private GreenCommandUnitMove _greenCommandUnitMove;

    public void Construct(IGreenCommandSpawner greenCommandSpawner) =>
           _greenCommandSpawner = greenCommandSpawner;

    private void Start()
    {
        GreenCommandUnitMove greenCommandUnitMove = _greenCommandSpawner.UnitsSpawnedQueue.Dequeue();
        _greenCommandUnitMove = greenCommandUnitMove;
    }

    private void Update()
    {
        if (_greenCommandUnitMove)
            if (Physics.Raycast(_greenCommandUnitMove.transform.position, _greenCommandUnitMove.transform.right, 1f))
                _greenCommandUnitMove.Move();
    }
}
