using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartSorting : MonoBehaviour
{
    [SerializeField]
    private GameObject anim;
    [SerializeField]
    private Button playPauseButton;

    public void OnButtonClick()
    {
        if (GetComponent<Button>().interactable)
            if (PlayerPrefs.GetString("algorithm").Contains("Sort"))
                anim.transform.GetChild(0).GetComponent<Sortings>().Restart();
            else if (PlayerPrefs.GetString("algorithm").Contains("Graph"))
            {
                anim.transform.GetChild(0).GetComponent<Graphs>().Restart();
                anim.transform.GetChild(0).GetChild(1).GetComponent<Structures>().Restart();
               
            }  
        playPauseButton.GetComponent<PlayPauseAnimation>().resetButtonText();
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("algorithm").Contains("Sort"))
            gameObject.SetActive(true);
        else if (PlayerPrefs.GetString("algorithm").Contains("Graph"))
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
        
        GetComponent<Button>().interactable = false;
    }
}
