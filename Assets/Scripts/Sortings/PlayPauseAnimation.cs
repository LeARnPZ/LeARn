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


    public void OnButtonClick()
    {
        string algorithm = PlayerPrefs.GetString("algorithm");

        if (algorithm.Contains("Sort"))
        {
            Sortings sort = animationObject.transform.GetChild(0).GetComponent<Sortings>();
            sort.Pause();

            if (sort.IsPaused())
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
            graph.Pause();

            if (graph.IsPaused())
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

    public void ResetButtonText()
    {
        playImage.gameObject.SetActive(false);
        pauseImage.gameObject.SetActive(true);
    }

    void Start()
    {
        playImage = playPauseButton.transform.Find("PlayImage");
        pauseImage = playPauseButton.transform.Find("PauseImage");

        string algorithm = PlayerPrefs.GetString("algorithm");
        if (algorithm.Contains("Sort") || algorithm.Contains("Graph"))
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);

        GetComponent<Button>().interactable = false;
    }
}
