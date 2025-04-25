using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class SpeedButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI speedText;

    [SerializeField]
    private GameObject animationObject;

    private float[] speeds = { 1.0f, 1.25f, 1.5f, 1.75f, 2.0f };
    private int currentSpeedIndex = 0;

    public void OnSpeedButtonClick()
    {
        Sortings sort = animationObject.transform.GetChild(0).GetComponent<Sortings>();

        if (!sort.IsPaused())
        {
            currentSpeedIndex = (currentSpeedIndex + 1) % speeds.Length;
            Time.timeScale = speeds[currentSpeedIndex];

            if (speedText != null)
            {
                speedText.text = speeds[currentSpeedIndex].ToString("0.##", CultureInfo.InvariantCulture) + "x";

            }
        }
    }

    public void SpeedButtonRestart()
    {
        currentSpeedIndex = 0;
        Time.timeScale = speeds[currentSpeedIndex];

        if (speedText != null)
        {
            speedText.text = speeds[currentSpeedIndex].ToString("0.##", CultureInfo.InvariantCulture) + "x";

        }
    }

    void OnDisable()
    {
        SpeedButtonRestart();
    }

    void Start()
    {
        string algorithm = PlayerPrefs.GetString("algorithm");

        // Poka¿ tylko, jeœli to Sort albo Graph
        if (algorithm.Contains("Sort") || algorithm.Contains("Graph"))
        {
            gameObject.SetActive(true);
            Time.timeScale = speeds[currentSpeedIndex];
            if (speedText != null)
            {
                speedText.text = $"{speeds[currentSpeedIndex]}x";
            }

            GetComponent<Button>().interactable = false;
        }
        else
        {
            gameObject.SetActive(false);
        }

        GetComponent<Button>().interactable = false;
    }
}
