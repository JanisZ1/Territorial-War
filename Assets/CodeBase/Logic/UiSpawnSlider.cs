using UnityEngine;
using UnityEngine.UI;

public class UiSpawnSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private QueueWarrior _queueWarrior;

    private void Start() =>
        _queueWarrior = FindObjectOfType<QueueWarrior>();

    private void Update()
    {
        if (_slider != null && _queueWarrior != null)
        {
            _slider.minValue = 0;
            _slider.maxValue = _queueWarrior.Delay;
        }
    }

    public void ChangeSliderValue(float currentValue) =>
        _slider.value = currentValue;
}
