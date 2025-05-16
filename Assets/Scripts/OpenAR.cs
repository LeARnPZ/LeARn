using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenAR : MonoBehaviour
{
    public void OnButtonClick()
    {
        if (name.StartsWith('!'))
        {
            StartCoroutine(FindAnyObjectByType<DemoNotification>(FindObjectsInactive.Include).ShowNotification());
        }
        else
        {
            PlayerPrefs.SetString("algorithm", name);
            SceneManager.LoadScene("ARScene");
        }
    }
}
