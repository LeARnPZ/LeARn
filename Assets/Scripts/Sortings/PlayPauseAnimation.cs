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

    private Transform pauseImage;
    private Transform playImage;


    public void onButtonClick()
    {

        Sortings sort = animationObject.transform.GetChild(0).GetComponent<Sortings>();
        bool isPaused = sort.getPaused();

 
        sort.Pause();

        if (sort.getPaused())
        {
            pauseImage.gameObject.SetActive(false);
            playImage.gameObject.SetActive(true);

        }
        else
        {
            playImage.gameObject.SetActive(false);
            pauseImage.gameObject.SetActive(true);
        }
        

        //Debug.Log("Przycisk Play/Pause klikniêty. Bool pauza = " + isPaused);
    }

    public void resetButtonText()
    {
        playImage.gameObject.SetActive(false);
        pauseImage.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        playImage = playPauseButton.transform.Find("PlayImage");
        pauseImage = playPauseButton.transform.Find("PauseImage");

        if (PlayerPrefs.GetString("algorithm").Contains("Sort"))
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);

        GetComponent<Button>().interactable = false;
    }
}
