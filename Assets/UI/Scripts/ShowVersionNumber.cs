using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowVersionNumber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Wersja: " + Application.version;
    }
}
