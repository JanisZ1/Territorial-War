using Assets.CodeBase.Infrastructure.Services.StaticData;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiSpawnSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private IStaticDataService _staticDataService;

    private void Start()
    {
        _slider.minValue = 0;
        _slider.maxValue = 3;
    }
    //TODO: Static data for ui
    public void Construct(IStaticDataService staticDataService)
    {
        _staticDataService = staticDataService;
    }

    private IEnumerator ChangeSliderValue(float delay)
    {
        _slider.maxValue = delay;
        while (true)
        {
            yield return null;

            _slider.value += Time.deltaTime;

            if (_slider.value >= delay)
            {
                ResetSlider();
                break;
            }
        }
    }

    private void ResetSlider() =>
        _slider.value = 0;

    public void StartChangeSliderValue(float delay) =>
         StartCoroutine(ChangeSliderValue(delay));
}
