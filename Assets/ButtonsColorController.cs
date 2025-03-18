using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsColorController : MonoBehaviour
{
    public Button button;

    public Color disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

    void Update()
    {
        if (!button.interactable)
        {
            // Wyszarzenie ca³ego przycisku i ikony
            button.image.color = disabledColor;
        }
        else
        {
            button.image.color = Color.white;
        }
    }
}
