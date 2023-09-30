public class GreenCommandUnitSpawner : IGreenCommandSpawner
{
    private GreenCommandUnitMove _previousUnit;

    public void Spawn(GreenCommandUnitMove playerUnit)
    {
        playerUnit.PreviousUnit = _previousUnit;

        _previousUnit = playerUnit;
    }
}
