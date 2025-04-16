using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOrientationManager : MonoBehaviour
{

    public GameObject portraitCanvas;
    public GameObject landscapeCanvas;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else 
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
        }
    }
}
