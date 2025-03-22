using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToggleInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject info;
    private int algorithmNo;
   
    private void HideInfo()
    {
       info.SetActive(false);
    }

    private void ShowInfo()
    {
        info.SetActive(true);
    }

    public void OnButtonClick()
    {
        if (info.activeSelf)
            HideInfo();
        else
            ShowInfo();
    }

    private void Start()
    {
        algorithmNo = Dictionaries.algorithms.GetValueOrDefault(PlayerPrefs.GetString("algorithm"));
        //info.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Dictionaries.descriptions.GetValueOrDefault(algorithmNo).ToString();
        info.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Dictionaries.descriptions.GetValueOrDefault(algorithmNo).ToString();

    }
}
