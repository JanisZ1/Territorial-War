using Assets.CodeBase.Logic.Spawners;
using Assets.CodeBase.StaticData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueUnit : MonoBehaviour
{
    [SerializeField] private UiSpawnSlider _uiSpawnSlider;
    [SerializeField] private List<float> _productionList = new List<float>();
    [SerializeField] private GreenCommandUnitSpawner _greenCommandUnitSpawner;
    [SerializeField] private Button _queueButton;
    public event Action UnitAdded;

    private int _maximumUnitsAdded = 5;

    private bool _unitProduced;

    [SerializeField] private UnitType _unitType;

    public float Delay { get; private set; } = 3;

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

        _greenCommandUnitSpawner.Spawn(_unitType, _greenCommandUnitSpawner.transform.position, Quaternion.identity);

        _unitProduced = false;
    }
}
