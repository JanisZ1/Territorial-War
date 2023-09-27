using System.Collections.Generic;

public class GreenCommandUnitSpawner : IGreenCommandSpawner
{
    public List<IGreenCommandUnit> UnitsSpawned { get; } = new List<IGreenCommandUnit>();

    private int _index;

    public void Spawn(GreenCommandUnitMove playerUnit)
    {
        playerUnit.Construct(this);
        playerUnit.Id = _index;
        _index++;
    }
}
