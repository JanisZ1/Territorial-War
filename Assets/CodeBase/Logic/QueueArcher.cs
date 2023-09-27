using Assets.CodeBase.Infrastructure.Services.Factory;
using System.Collections.Generic;
using UnityEngine;

public class QueueArcher : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private UiSpawnSlider _uiSpawnSlider;

    private float _currentDelay;
    private List<float> _list = new List<float>();
    private bool _isFree = true;
    private bool _unitHasSpawned;
    private IArcherFactory _archerFactory;
    private IGreenCommandSpawner _greenCommandSpawner;

    public float Delay { get; private set; } = 3;

    public void Construct(IArcherFactory archerFactory, IGreenCommandSpawner greenCommandSpawner)
    {
        _archerFactory = archerFactory;
        _greenCommandSpawner = greenCommandSpawner;
    }

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
            GameObject newArcher = _archerFactory.CreateArcher(_playerPrefab, _spawnPosition.position, _spawnPosition.rotation);
            _greenCommandSpawner.AddToDictionary(newArcher.GetComponentInChildren<PlayerUnit>());
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
