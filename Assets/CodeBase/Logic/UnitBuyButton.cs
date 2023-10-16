using Assets.CodeBase.StaticData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitBuyButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private UnitType _unitType;

    public event Action<UnitType> Clicked;

    private void OnEnable() =>
        _button.onClick.AddListener(AddUnitToQueue);

    private void OnDisable() =>
        _button.onClick.RemoveListener(AddUnitToQueue);

    private void AddUnitToQueue() =>
        Clicked?.Invoke(_unitType);
}
