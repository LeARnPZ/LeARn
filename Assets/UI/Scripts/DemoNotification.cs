using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoNotification : MonoBehaviour
{
    private bool coroutineRunning = false;
    public IEnumerator ShowNotification()
    {
        if (coroutineRunning)
            yield break;

        coroutineRunning = true;
        gameObject.SetActive(true);
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FadeOut());
        gameObject.SetActive(false);
        coroutineRunning = false;
    }

    private IEnumerator FadeIn(float fadeDuration = 0.5f)
    {
        if (gameObject == null)
            yield break;

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogWarning("No CanvasGroup found on " + name);
            yield break;
        }

        canvasGroup.alpha = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut(float fadeDuration = 0.5f)
    {
        if (gameObject == null)
            yield break;

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogWarning("No CanvasGroup found on " + name);
            yield break;
        }

        canvasGroup.alpha = 1f;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}
