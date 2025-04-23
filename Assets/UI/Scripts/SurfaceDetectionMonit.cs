using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutMonit : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float fadeDuration = 2f; 

    private void Start()
    {

        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3f);
        canvasGroup.alpha = 1f;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false); 
    }
}
