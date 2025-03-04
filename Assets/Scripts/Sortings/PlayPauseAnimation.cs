using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayPauseAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject animationObject;
    [SerializeField]
    private Button playPauseButton;

    private TextMeshProUGUI buttonText;


    public void onButtonClick()
    {

        Sortings sort = animationObject.transform.GetChild(0).GetComponent<Sortings>();
        bool isPaused = sort.getPaused();

         //Debug.Log("Bool pauza = " + isPaused);
 
        sort.Pause();

        if (sort.getPaused())
        {
            buttonText.text = "PLAY";
        }
        else
        {
            buttonText.text = "PAUZA";
        }
        

        //Debug.Log("Przycisk Play/Pause klikniêty. Bool pauza = " + isPaused);
    }

    public void resetButtonText()
    {
        buttonText.text = "PAUZA";
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonText = playPauseButton.GetComponentInChildren<TextMeshProUGUI>();
        if (PlayerPrefs.GetString("algorithm").Contains("Sort"))
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);

        GetComponent<Button>().interactable = false;
    }
}
