using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class SliderHandleClickListener : MonoBehaviour, IPointerDownHandler
{

    public Slider slider;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (slider == null)
            return;
            
        if (PlayerPrefs.GetInt("PoorMode") == 1 && slider.CompareTag("VisibilitySlider"))
            return;

        if (slider.value == 0)
        {
            slider.value = 1;
        }
        else if (slider.value == 1)
        {
            slider.value = 0;
        }
    }
}
