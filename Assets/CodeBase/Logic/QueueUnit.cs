using Assets.CodeBase.Infrastructure.Services.Factory.Spawners;
using Assets.CodeBase.StaticData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueUnit : MonoBehaviour
{
    [SerializeField] private UiSpawnSlider _uiSpawnSlider;
    [SerializeField] private List<float> _productionList = new List<float>();
    [SerializeField] private Button _queueButton;
    [SerializeField] private UnitType _unitType;

    public event Action UnitAdded;

    private int _maximumUnitsAdded = 5;
    private bool _unitProduced;
    private IHumanUnitSpawnerFactory _spawnersFactory;

    public float Delay { get; private set; } = 3;

    public void Construct(IHumanUnitSpawnerFactory spawnersFactory) =>
        _spawnersFactory = spawnersFactory;

    private void OnEnable() =>
        _queueButton.onClick.AddListener(AddUnit);

    private void OnDisable() =>
        _queueButton.onClick.RemoveListener(AddUnit);

    private void AddUnit()
    {
        if (_productionList.Count < _maximumUnitsAdded)
            _productionList.Add(Delay);
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
        _uiSpawnSlider.StartChangeSliderValue();
        Invoke(nameof(CreateUnit), _productionList[0]);
    }

    private void CreateUnit()
    {
        _productionList.RemoveAt(0);

        _spawnersFactory.UnitSpawner.Spawn(_unitType, _spawnersFactory.UnitSpawner.transform.position, Quaternion.identity);

        _unitProduced = false;
    }
}
