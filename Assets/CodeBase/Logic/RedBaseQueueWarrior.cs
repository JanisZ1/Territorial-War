using Assets.CodeBase.Infrastructure.Services.Factory;
using Assets.CodeBase.StaticData;
using System.Collections.Generic;
using UnityEngine;

public class RedBaseQueueWarrior : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    private List<MeleeAttack> _spawnedUnits = new List<MeleeAttack>();
    private List<float> _list = new List<float>();

    private float _currentDelay;
    private bool _isFree = true;
    private bool _uUnitHasSpawned;
    private IGreenCommandUnitFactory _warriorFactory;
    [SerializeField] private UnitType _unitType;

    public float Delay { get; private set; } = 3;

    public void Construct(IGreenCommandUnitFactory warriorFactory) =>
        _warriorFactory = warriorFactory;

    public void AddedUnit()
    {
        _list.Add(Delay);
        if (_isFree)
        {
            StartNext();
        }
    }

    private void CreateUnit()
    {
        if (_spawnPosition != null)
        {
            _list.RemoveAt(0);
            //TODO: Static data for different warriors
            GameObject warrior = _warriorFactory.CreateUnit(_unitType, _spawnPosition.position, _spawnPosition.rotation);
            _spawnedUnits.Add(warrior.GetComponent<MeleeAttack>());
            _isFree = true;
            _currentDelay = 0;
            StartNext();
        }
    }

    private void StartNext()
    {
        if (_list.Count > 0)
        {
            _isFree = false;
            Invoke(nameof(CreateUnit), _list[0]);
        }
        else
        {
            _isFree = true;
        }
    }

    private void Update()
    {
        if (_isFree == false && GetComponent<CanvasRenderer>())
            _currentDelay += Time.deltaTime;
    }
}
