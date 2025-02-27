using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenAR : MonoBehaviour
{
    public void OnButtonClick()
    {
        PlayerPrefs.SetString("algorithm", name);
        SceneManager.LoadScene("ARScene");
    }
}
