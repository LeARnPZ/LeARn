using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI speedText;

    private float[] speeds = { 1.0f, 1.25f, 1.5f, 1.75f, 2.0f };
    private int currentSpeedIndex = 0;

    public void OnSpeedButtonClick()
    {
        currentSpeedIndex = (currentSpeedIndex + 1) % speeds.Length;
        Time.timeScale = speeds[currentSpeedIndex];

        if (speedText != null)
        {
            speedText.text = $"{speeds[currentSpeedIndex]}x";
        }
    }

    public void SpeedButtonRestart()
    {
        currentSpeedIndex = 0;
        Time.timeScale = speeds[currentSpeedIndex];

        if (speedText != null)
        {
            speedText.text = $"{speeds[currentSpeedIndex]}x";
        }
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
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
