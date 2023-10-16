using Assets.CodeBase.Infrastructure.Services.Factory.Spawners;
using Assets.CodeBase.StaticData;
using System.Collections.Generic;
using UnityEngine;

public class QueueUnit : MonoBehaviour
{
    [SerializeField] private UiSpawnSlider _uiSpawnSlider;
    private List<UnitType> _productionList = new List<UnitType>();
    [SerializeField] private UnitBuyButton[] _unitBuyButtons;

    private int _maximumUnitsAdded = 5;
    private bool _unitProduced;
    private IHumanUnitSpawnerFactory _spawnersFactory;

    public float Delay { get; private set; } = 3;

    public void Construct(IHumanUnitSpawnerFactory spawnersFactory) =>
        _spawnersFactory = spawnersFactory;

    private void Start()
    {
        foreach (UnitBuyButton unitBuyButton in _unitBuyButtons)
            unitBuyButton.Clicked += AddUnit;
    }

    private void OnDestroy()
    {
        foreach (UnitBuyButton unitBuyButton in _unitBuyButtons)
            unitBuyButton.Clicked -= AddUnit;
    }

    public void AddUnit(UnitType unitType)
    {
        if (_productionList.Count < _maximumUnitsAdded)
            _productionList.Add(unitType);
    }

    private bool UnitsIsInProduction() =>
        _productionList.Count > 0;

    private void Update()
    {
        if (UnitsIsInProduction() && !_unitProduced)
        {
            _unitProduced = true;
            StartNext();
        }
    }

    private void StartNext()
    {
        _uiSpawnSlider.StartChangeSliderValue(Delay);
        Invoke(nameof(CreateUnit), Delay);
    }

    private void CreateUnit()
    {
        _spawnersFactory.UnitSpawner.Spawn(_productionList[0], _spawnersFactory.UnitSpawner.transform.position, Quaternion.identity);

        _productionList.RemoveAt(0);

        _unitProduced = false;
    }
}
