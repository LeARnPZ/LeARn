using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShowDate : MonoBehaviour
{
    void Start()
    {
        string todayDate = DateTime.Now.ToString("dd.MM.yyyy");
        GetComponent<TextMeshProUGUI>().text = "Data ostatniej \r\naktualizacji: " + todayDate + " r.\r\n\r\nAplikacja zosta�a stworzona w ramach zaj�� \"Programowanie Zespo�owe\" na Uniwersytecie Miko�aja Kopernika w Toruniu w roku akademickim 2024/2025.";
    }

}
