using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider _healthSlider;
    [SerializeField]
    private Image _fillImage;

    [SerializeField]
    private Sprite _healthBarSprite;
    [SerializeField]
    private Sprite _resBarSprite;

    private Coroutine _sliderChangeRoutine;
    private const float SliderChangeTime = 0.8f;

    public void SetAlive(bool isAlive)
    {
        _fillImage.sprite = isAlive ? _healthBarSprite : _resBarSprite;
    }

    public void SetSliderValue(float val)
    {
        if (val >= 0.0f && val <= 1.0f)
        {
            if (_sliderChangeRoutine != null)
            {
                StopCoroutine(_sliderChangeRoutine);
            }
            _sliderChangeRoutine = StartCoroutine(ChangeSliderOverTime(val, SliderChangeTime));
        }
    }

    private IEnumerator ChangeSliderOverTime(float val, float time)
    {
        float t = 0.0f;
        float normalizer = 1 / time;
        float initialVal = _healthSlider.value;
        while (t < 1.0f)
        {
            t = Mathf.Min(t + Time.deltaTime * normalizer, 1.0f);
            _healthSlider.value = Mathf.Lerp(initialVal, val, t);
            yield return null;
        }
        _sliderChangeRoutine = null;
    }
}
