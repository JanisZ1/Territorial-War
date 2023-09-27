using System.Collections.Generic;

public class GreenCommandUnitSpawner : IGreenCommandSpawner
{
    public Dictionary<PlayerUnit, int> UnitsSpawned { get; } = new Dictionary<PlayerUnit, int>();

    private int _index;

    public void AddToDictionary(PlayerUnit playerUnit)
    {
        UnitsSpawned.Add(playerUnit, _index);
        _index++;
    }
}
