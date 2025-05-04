using UnityEngine;
using TMPro;

public class ShowDate : MonoBehaviour
{
    void Start()
    {
        string todayDate = BuildInfo.BuildDate;
        GetComponent<TextMeshProUGUI>().text =
            "Data ostatniej \r\naktualizacji: " + todayDate + " r.\r\n\r\n" +
            "Aplikacja zosta�a stworzona w ramach zaj�� \"Programowanie Zespo�owe\" " +
            "na Uniwersytecie Miko�aja Kopernika w Toruniu w roku akademickim 2024/2025.";
    }
}
