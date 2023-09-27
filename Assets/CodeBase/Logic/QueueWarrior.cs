using Assets.CodeBase.Infrastructure.Services.Factory;
using System.Collections.Generic;
using UnityEngine;

public class QueueWarrior : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private UiSpawnSlider _uiSpawnSlider;
    [SerializeField] private List<MeleeAttack> _spawnedUnits = new List<MeleeAttack>();
    [SerializeField] private List<float> _list = new List<float>();

    private float _currentDelay;
    private bool _isFree = true;
    private bool _uUnitHasSpawned;
    private IWarriorFactory _warriorFactory;
    private IGreenCommandSpawner _greenCommandSpawner;

    public float Delay { get; private set; } = 3;

    public void Construct(IWarriorFactory warriorFactory, IGreenCommandSpawner greenCommandSpawner)
    {
        _warriorFactory = warriorFactory;
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
            //TODO: Static data for different warriors
            GameObject warrior = _warriorFactory.CreateWarrior(_playerPrefab, _spawnPosition.position, _spawnPosition.rotation);
            _spawnedUnits.Add(warrior.GetComponent<MeleeAttack>());
            _greenCommandSpawner.Spawn(warrior.GetComponentInChildren<GreenCommandUnitMove>());
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
        if (!_playerPrefab || !_spawnPosition || !_uiSpawnSlider)
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
