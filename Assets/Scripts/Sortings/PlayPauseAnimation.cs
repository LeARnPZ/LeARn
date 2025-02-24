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
        BubbleSort bubbleSort = animationObject.transform.GetChild(0).GetComponent<BubbleSort>();
        bool isPaused = bubbleSort.getPaused();

        playPauseButton.interactable = true;


        Debug.Log("Bool pauza = " + isPaused);
        isPaused = !isPaused;
        bubbleSort.setPaused(isPaused);

        if (bubbleSort.getPaused())
        {
            buttonText.text = "PLAY";
        }
        else
        {
            buttonText.text = "PAUZA";
        }
        

        Debug.Log("Przycisk Play/Pause klikniêty. Bool pauza = " + isPaused);
    }

    public void resetButtonText()
    {
        buttonText.text = "PAUZA";
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonText = playPauseButton.GetComponentInChildren<TextMeshProUGUI>();
        playPauseButton.interactable = false;
    }
}
