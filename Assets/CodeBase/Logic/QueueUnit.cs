using Assets.CodeBase.Logic.Spawners;
using Assets.CodeBase.StaticData;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueUnit : MonoBehaviour
{
    [SerializeField] private UiSpawnSlider _uiSpawnSlider;
    [SerializeField] private List<float> _list = new List<float>();
    [SerializeField] private GreenCommandUnitSpawner _greenCommandUnitSpawner;
    [SerializeField] private Button _queueButton;

    private float _currentDelay;
    private bool _isFree = true;

    [SerializeField] private UnitType _unitType;

    public float Delay { get; private set; } = 3;

    private void OnEnable() =>
        _queueButton.onClick.AddListener(AddUnit);

    private void OnDisable() =>
        _queueButton.onClick.RemoveListener(AddUnit);

    private void AddUnit()
    {
        _list.Add(Delay);
        if (_isFree)
        {
            StartNext();
        }
    }

    private void CreateUnit()
    {
        _list.RemoveAt(0);

        _greenCommandUnitSpawner.Spawn(_unitType, _greenCommandUnitSpawner.transform.position, Quaternion.identity);

        _isFree = true;
        _currentDelay = 0;
        StartNext();
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
        if (_isFree == false)
        {
            _currentDelay += Time.deltaTime;
            _uiSpawnSlider.ChangeSliderValue(_currentDelay);
        }
        else if (_isFree == true)
        {
            _uiSpawnSlider.ChangeSliderValue(_currentDelay);
        }
    }
}
