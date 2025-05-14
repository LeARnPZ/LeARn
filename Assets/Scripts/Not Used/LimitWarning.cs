using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LimitWarning : MonoBehaviour
{
    [SerializeField]
    private GameObject image;
    [SerializeField]
    private GameObject text;

    private void FadeIn(float duration)
    {
        image.GetComponent<Image>().CrossFadeAlpha(0.8f, duration, false);
        text.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0.8f, duration, false);
    }

    private void FadeOut(float duration)
    {
        image.GetComponent<Image>().CrossFadeAlpha(0, duration, false);
        text.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0, duration, false);
    }

    public IEnumerator ShowWarning()
    {
        image.SetActive(true);
        FadeIn(0.5f);
        yield return new WaitForSeconds(1);
        FadeOut(0.5f);
        yield return new WaitForSeconds(1);
        image.SetActive(false);
    }

    private void OnEnable()
    {
        FadeOut(0);
        image.GetComponent<Image>().color = Color.black;
        text.GetComponent<TextMeshProUGUI>().color = Color.white;
    }
}
