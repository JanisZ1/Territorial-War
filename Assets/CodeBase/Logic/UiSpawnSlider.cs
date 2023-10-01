using Assets.CodeBase.Infrastructure.Services.StaticData;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiSpawnSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private QueueUnit _queueUnit;
    private IStaticDataService _staticDataService;

    private void Start()
    {
        _slider.minValue = 0;
        _slider.maxValue = _queueUnit.Delay;
    }
    //TODO: Static data for ui
    public void Construct(IStaticDataService staticDataService)
    {
        _staticDataService = staticDataService;
    }

    private IEnumerator ChangeSliderValue()
    {
        while (true)
        {
            yield return null;

            _slider.value += Time.deltaTime;

            if (_slider.value >= _queueUnit.Delay)
            {
                ResetSlider();
                break;
            }
        }
    }

    private void ResetSlider() =>
        _slider.value = 0;

    public void StartChangeSliderValue() =>
         StartCoroutine(ChangeSliderValue());
}
