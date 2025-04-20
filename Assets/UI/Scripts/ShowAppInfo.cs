using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAppInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject appInfo;

    [SerializeField]
    private GameObject optionsBackground;

    public void OnButtonClick()
    {
        appInfo.SetActive(true);
        optionsBackground.SetActive(false);
    }

    public void OnBackButtonClick()
    {
        appInfo.SetActive(false);
    }
}
