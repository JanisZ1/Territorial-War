using System.Collections.Generic;

public class GreenCommandUnitSpawner : IGreenCommandSpawner
{
    public List<PlayerUnit> UnitsSpawned { get; } = new List<PlayerUnit>();

    private int _index;

    public void Spawn(PlayerUnit playerUnit)
    {
        UnitsSpawned.Add(playerUnit);
        playerUnit.Construct(this);
        playerUnit.Id = _index;
        _index++;
    }
}
