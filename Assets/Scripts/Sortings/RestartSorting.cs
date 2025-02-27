using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartSorting : MonoBehaviour
{
    [SerializeField]
    private new GameObject animation;

    public void OnButtonClick()
    {
        if (GetComponent<Button>().interactable) 
            animation.transform.GetChild(0).GetComponent<Sortings>().Restart();
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("algorithm").Contains("Sort"))
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
        
        GetComponent<Button>().interactable = false;
    }
}
