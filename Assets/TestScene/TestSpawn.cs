using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    [SerializeField] private TestUnit _testUnit;
    [SerializeField] private Transform _spawn;
    [SerializeField] private int _unitIDCount;
    private void Start()
    {
        _unitIDCount = 0;
        SpawnUnits();
        
    }
    private void SpawnUnits()
    {
        for (int i = 0; i < 3; i++)
        {
            TestUnit newUnit = Instantiate(_testUnit, _spawn.position, Quaternion.identity);
            newUnit.Initialize(_unitIDCount++);
            Debug.Log(_unitIDCount);
        }
        
    }
}
