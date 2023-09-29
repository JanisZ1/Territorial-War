using System.Collections.Generic;

public class GreenCommandUnitSpawner : IGreenCommandSpawner
{
    //TODO: Make One interface or class to green and red commandUnits
    public List<GreenCommandUnitMove> AllUnitsClipped { get; } = new List<GreenCommandUnitMove>();

    private int _index;

    public void Spawn(GreenCommandUnitMove playerUnit)
    {
        playerUnit.Construct(this);
        playerUnit.Id = _index;

        _index++;
    }
}
