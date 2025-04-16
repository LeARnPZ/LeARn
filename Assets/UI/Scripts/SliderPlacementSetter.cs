using UnityEngine;
using UnityEngine.UI;

public class SliderPlacementSetter : MonoBehaviour
{
    public Slider placementSlider;

    private void Start()
    {
        int savedValue = PlayerPrefs.GetInt("placement", 0);

        placementSlider.onValueChanged.RemoveAllListeners();
        placementSlider.value = savedValue;

        placementSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnSliderValueChanged(float value)
    {
        int placementValue = value >= 0.5f ? 1 : 0;
        PlayerPrefs.SetInt("placement", placementValue);
        PlayerPrefs.Save();
    }
}
