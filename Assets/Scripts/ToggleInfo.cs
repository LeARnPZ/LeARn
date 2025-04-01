using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToggleInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject info;

    [SerializeField]
    private GameObject infoStructs;

    private int algorithmNo;
    private bool isShowingSteps = false;
   
    private void HideInfo()
    {
       info.SetActive(false);
    }

    private void ShowInfo()
    {
        if (isShowingSteps)
        {
            info.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Dictionaries.stepBySteps.GetValueOrDefault(algorithmNo).ToString();
        }
        else
        {
            info.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Dictionaries.descriptions.GetValueOrDefault(algorithmNo).ToString();
        }

        info.SetActive(true);
    }

    public void OnButtonClick()
    {
        if (info.activeSelf)
            HideInfo();
        else
            ShowInfo();
    }

    public void OnStepsButtonClick()
    {
        isShowingSteps = true;
        info.transform.GetChild(1).gameObject.SetActive(false);
        info.transform.GetChild(2).gameObject.SetActive(true);
        ShowInfo();
    }

    public void OnDescriptionButtonClick()
    {
        isShowingSteps = false;
        info.transform.GetChild(1).gameObject.SetActive(true);
        info.transform.GetChild(2).gameObject.SetActive(false);
        ShowInfo();
    }

    private void Start()
    {
        algorithmNo = Dictionaries.algorithms.GetValueOrDefault(PlayerPrefs.GetString("algorithm"));
        info.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Dictionaries.descriptions.GetValueOrDefault(algorithmNo).ToString();

        if (PlayerPrefs.GetString("algorithm").Contains("Struct")){
            info = infoStructs;
        }

    }
}
