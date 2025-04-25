using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePlaceObjectMode : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        if (!PlayerPrefs.HasKey("PoorMode"))
        {
            PlayerPrefs.SetInt("PoorMode", 0);
            slider.value = 1;
            PlayerPrefs.Save();
        }
        else
        {
            slider.value = (PlayerPrefs.GetInt("PoorMode") + 1) % 2;
        }
    }

    public void OnSliderValueChange()
    {
        if (slider.value == 0)
            PlayerPrefs.SetInt("PoorMode", 1);
        else if (slider.value == 1)
            PlayerPrefs.SetInt("PoorMode", 0);

        PlayerPrefs.Save();
    }
}
