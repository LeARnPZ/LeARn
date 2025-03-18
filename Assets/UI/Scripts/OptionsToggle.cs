using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsToggle : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsImage;

    private void HideOptions()
    {
        optionsImage.SetActive(false);
    }

    private void ShowOptions()
    {
        optionsImage.SetActive(true);
    }

    public void OnButtonClick()
    {
        if (optionsImage.activeSelf)
            HideOptions();
        else
            ShowOptions();
    }
}
