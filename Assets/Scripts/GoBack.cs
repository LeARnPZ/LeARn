using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour
{
    public void OnButtonClick()
    {
        if (SceneManager.GetActiveScene().name == "ARScene")
            SceneManager.LoadScene("MainMenu");
    }
}
