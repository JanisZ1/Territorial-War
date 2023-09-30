using Assets.CodeBase.Infrastructure.Services.Factory;
using System.Collections.Generic;
using UnityEngine;

public class QueueWarrior : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private UiSpawnSlider _uiSpawnSlider;
    [SerializeField] private List<MeleeAttack> _spawnedUnits = new List<MeleeAttack>();
    [SerializeField] private List<float> _list = new List<float>();
    [SerializeField] private GreenCommandUnitSpawner _greenCommandUnitSpawner;

    private float _currentDelay;
    private bool _isFree = true;
    private bool _uUnitHasSpawned;
    private IWarriorFactory _warriorFactory;

    public float Delay { get; private set; } = 3;

    public void AddedUnit()
    {
        if (_uiSpawnSlider != null)
        {
            _list.Add(Delay);
            if (_isFree)
            {
                StartNext();
            }
        }
    }

    private void CreateUnit()
    {
        _list.RemoveAt(0);
        //TODO: Static data for different warriors
        GreenCommandUnitMove greenCommandUnitMove = _greenCommandUnitSpawner.Spawn(_playerPrefab, _greenCommandUnitSpawner.transform.position, Quaternion.identity);
        _spawnedUnits.Add(greenCommandUnitMove.GetComponent<MeleeAttack>());

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
        if (!_playerPrefab || !_uiSpawnSlider)
            return;

        if (_isFree == false && GetComponent<CanvasRenderer>())
        {
            _currentDelay += Time.deltaTime;
            _uiSpawnSlider.ChangeSliderValue(_currentDelay);
        }
        else if (_isFree == true && GetComponent<CanvasRenderer>())
        {
            _uiSpawnSlider.ChangeSliderValue(_currentDelay);
        }
    }
}
