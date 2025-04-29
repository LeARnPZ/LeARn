using UnityEngine;
using TMPro;

public class ShowDate : MonoBehaviour
{
    void Start()
    {
        string todayDate = BuildInfo.BuildDate;
        GetComponent<TextMeshProUGUI>().text =
            "Data ostatniej \r\naktualizacji: " + todayDate + " r.\r\n\r\n" +
            "Aplikacja zosta³a stworzona w ramach zajêæ \"Programowanie Zespo³owe\" " +
            "na Uniwersytecie Miko³aja Kopernika w Toruniu w roku akademickim 2024/2025.";
    }
}
