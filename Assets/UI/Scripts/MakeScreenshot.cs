using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeScreenshot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ScreenCapture.CaptureScreenshot("screenshot.png");
            Debug.Log("Zrzut ekranu zapisany!");
        }
    }
}
