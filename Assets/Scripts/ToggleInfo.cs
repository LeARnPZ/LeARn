using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject info;

    [SerializeField]
    private GameObject infoStructs;
    [SerializeField]
    private GameObject optionsImage;
    [SerializeField]
    private Image complexityImage;
    private Sprite complexitySprite;

    private int algorithmNo;
    private bool isShowingSteps = false;
    private bool isStruct = false;
   
    private void HideInfo()
    {
       info.SetActive(false);
    }

    private void ShowInfo()
    {
        if (isShowingSteps)
        {
            info.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Dictionaries.stepBySteps.GetValueOrDefault(algorithmNo).ToString();
            info.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().lineSpacing = 15f;
        }
        else
        {
            info.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Dictionaries.descriptions.GetValueOrDefault(algorithmNo).ToString();

            if (!isStruct)
            {
                string complexityText = Dictionaries.complexityValues.GetValueOrDefault(algorithmNo).ToString().Trim();

                info.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = complexityText;
                switch (complexityText)
                {
                    case "Θ(V + E)":
                    case "Θ(n)":
                        complexitySprite = Resources.Load<Sprite>("Sprites/" + "complexity_bar_bright-green");
                        break;

                    case "Θ(n log n)":
                        complexitySprite = Resources.Load<Sprite>("Sprites/" + "complexity_bar_yellow");
                        break;

                    case "Θ(n²)":
                    case "Θ(V²)":
                        complexitySprite = Resources.Load<Sprite>("Sprites/" + "complexity_bar_orange");
                        break;
                }

                if(complexitySprite != null)
                {
                    complexityImage.sprite = complexitySprite;
                }
                else
                {
                    Debug.LogWarning("Sprite nie został znaleziony!");
                }
            }
        }

        if (optionsImage.activeSelf)
        {
            optionsImage.SetActive(false);
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
            isStruct = true;
        }
        else
        {
            info.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = Dictionaries.complexityValues.GetValueOrDefault(algorithmNo).ToString();
        }

    }
}
