using System.Collections.Generic;

public class GreenCommandUnitSpawner : IGreenCommandSpawner
{
    public List<GreenCommandUnitMove> UnitsSpawned { get; } = new List<GreenCommandUnitMove>();

    private int _index;

    public void Spawn(GreenCommandUnitMove playerUnit)
    {
        UnitsSpawned.Add(playerUnit);
        playerUnit.Construct(this);
        playerUnit.Id = _index;
        _index++;
    }
}
