using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField]
    private GameObject anim;

    public void OnButtonClick()
    {
        if (GetComponent<Button>().interactable)
            anim.transform.GetChild(0).GetComponent<SelectSort>().Pause();
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("algorithm").Contains("Sort"))
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);

        GetComponent<Button>().interactable = false;
    }
}
