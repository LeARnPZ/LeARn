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
        string algorithm = PlayerPrefs.GetString("algorithm");

        if (algorithm.Contains("Sort"))
        {
            Sortings sort = animationObject.transform.GetChild(0).GetComponent<Sortings>();
            bool isPaused = sort.getPaused();
            sort.Pause();
            isPaused = sort.getPaused();

            if (isPaused)
            {
                pauseImage.gameObject.SetActive(false);
                playImage.gameObject.SetActive(true);
            }
            else
            {
                playImage.gameObject.SetActive(false);
                pauseImage.gameObject.SetActive(true);
            }
        }
        else if (algorithm.Contains("Graph"))
        {
            Graphs graph = animationObject.transform.GetChild(0).GetComponent<Graphs>();
            bool isPaused = graph.getPaused();
            graph.Pause();
            isPaused = graph.getPaused();

            if (isPaused)
            {
                pauseImage.gameObject.SetActive(false);
                playImage.gameObject.SetActive(true);
            }
            else
            {
                playImage.gameObject.SetActive(false);
                pauseImage.gameObject.SetActive(true);
            }
        }
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
        else if(PlayerPrefs.GetString("algorithm").Contains("Graph"))
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);

        GetComponent<Button>().interactable = false;
    }
}
