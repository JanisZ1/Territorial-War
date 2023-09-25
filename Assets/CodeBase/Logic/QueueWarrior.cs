using System.Collections.Generic;
using UnityEngine;

public class QueueWarrior : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private UiSpawnSlider _uiSpawnSlider;
    public List<PlayerUnit> _spawnedUnits = new List<PlayerUnit>();
    public float _delay { get; private set; } = 3;
    private float _currentDelay;
    [SerializeField] private List<float> list = new List<float>();
    private bool _isFree = true;
    private bool _uUnitHasSpawned;
    public void AddedUnit()
    {
        if (_uiSpawnSlider != null)
        {
            list.Add(_delay);
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
            list.RemoveAt(0);
            GameObject newPlayerUnit = Instantiate(_playerPrefab, _spawnPosition.position, _spawnPosition.rotation);
            _spawnedUnits.Add(newPlayerUnit.GetComponent<PlayerUnit>());
            _isFree = true;
            _currentDelay = 0;
            StartNext();
        }
    }

    private void StartNext()
    {
        if (list.Count > 0)
        {
            _isFree = false;
            Invoke(nameof(CreateUnit), list[0]);
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
