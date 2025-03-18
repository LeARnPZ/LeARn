using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableSlider : MonoBehaviour, IPointerDownHandler
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransform sliderRect = slider.GetComponent<RectTransform>();
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(sliderRect, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            float normalizedValue = Mathf.InverseLerp(sliderRect.rect.xMin, sliderRect.rect.xMax, localPoint.x);
            slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, normalizedValue);
        }
    }
}
