using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasterEgg : MonoBehaviour
{
    private int clicks;
    private float timeElapsed;

    public void IncreaseClicks()
    {
        if (timeElapsed > 1)
        {
            clicks = 1;
            timeElapsed = 0;
        }
        else
            clicks++;
    }

    private void Start()
    {
        clicks = 0;
        timeElapsed = 0;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (clicks > 4)
            SceneManager.LoadScene("EasterEgg");
    }
}
