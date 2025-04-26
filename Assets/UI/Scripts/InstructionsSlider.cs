using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsSlider : MonoBehaviour
{
    [SerializeField]
    private Slider instructionsSlider;

    void Start()
    {
        if (PlayerPrefs.HasKey("InstructionsEnabled"))
        {
            int enabled = PlayerPrefs.GetInt("InstructionsEnabled", 1);
            instructionsSlider.value = enabled;
            Debug.Log("Start: Odczytano InstructionsEnabled = " + enabled);
        }
        else
        {
            PlayerPrefs.SetInt("InstructionsEnabled", 1);
            PlayerPrefs.Save();
            instructionsSlider.value = 1f;
            Debug.Log("Start: Nie znaleziono klucza. Ustawiono InstructionsEnabled = 1");
        }
    }

    public void OnSliderChanged(float value)
    {
        int intValue = Mathf.RoundToInt(value);
        PlayerPrefs.SetInt("InstructionsEnabled", intValue);
        PlayerPrefs.Save();
        Debug.Log("OnSliderChanged: Zmieniono InstructionsEnabled na " + intValue);
    }
}
