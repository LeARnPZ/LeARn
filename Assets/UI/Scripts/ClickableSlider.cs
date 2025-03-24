using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickableSlider : MonoBehaviour, IPointerDownHandler
{
    private Slider slider;
    private RectTransform handleRect;

    void Start()
    {
        slider = GetComponent<Slider>();

        // Pobranie RectTransform z uchwytu (Handle)
        if (slider.handleRect != null)
        {
            handleRect = slider.handleRect;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransform sliderRect = slider.GetComponent<RectTransform>();
        Vector2 localPoint;

        // Jeœli klikniêto na Handle, zmieñ wartoœæ suwaka
        if (handleRect != null && RectTransformUtility.RectangleContainsScreenPoint(handleRect, eventData.position, eventData.pressEventCamera))
        {
            SetSliderValue(eventData);
        }
        // Jeœli klikniêto na suwak, ale poza Handle
        else if (RectTransformUtility.ScreenPointToLocalPointInRectangle(sliderRect, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            SetSliderValue(eventData);
        }
    }

    private void SetSliderValue(PointerEventData eventData)
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
