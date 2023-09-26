using System.Collections.Generic;
using UnityEngine;

public class QueueWarrior : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private UiSpawnSlider _uiSpawnSlider;
    [SerializeField] private List<PlayerUnit> _spawnedUnits = new List<PlayerUnit>();
    [SerializeField] private List<float> _list = new List<float>();

    private float _currentDelay;
    private bool _isFree = true;
    private bool _uUnitHasSpawned;

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
        if (_spawnPosition != null)
        {
            _list.RemoveAt(0);
            GameObject newPlayerUnit = Instantiate(_playerPrefab, _spawnPosition.position, _spawnPosition.rotation);
            _spawnedUnits.Add(newPlayerUnit.GetComponent<PlayerUnit>());
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
