using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using System.Linq;


public class InstructionsManager : MonoBehaviour
{
    [Header("Instructions Images")]
    public GameObject scanSurfaceInstruction;
    public GameObject scanErrorInstruction;
    public GameObject touchScreenInstruction;
    public GameObject scaleInstruction;
    public GameObject moveIteratorInstruction;

    [Header("Other")]
    public ARPlaneManager planeManager;
    public PlaceObject placeObjectScript;

    [Header("Settings")]
    public float minPlaneSize = 0.3f;
    public float instructionDuration = 2f;
    public float searchDuration = 5f;

    private bool instructionsEnabled;

    // Start is called before the first frame update
    void Start()
    {
        instructionsEnabled = PlayerPrefs.GetInt("InstructionsEnabled", 1) == 1;
        StartCoroutine(HandleInstructions());
    }

    private bool IsSurfaceDetected()
    {
        foreach (var plane in planeManager.trackables)
        {
            if (plane.size.x >= minPlaneSize && plane.size.y >= minPlaneSize)
                return true;
        }
        return false;
    }

    private IEnumerator FadeOut(GameObject instructionBox, float fadeDuration = 0.5f, float delay = 0f)
    {
        if (instructionBox == null) yield break;

        CanvasGroup canvasGroup = instructionBox.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogWarning("No CanvasGroup found on " + instructionBox.name);
            yield break;
        }

        yield return new WaitForSeconds(delay);

        canvasGroup.alpha = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        instructionBox.SetActive(false);
    }

    private IEnumerator FadeIn(GameObject instructionBox, float fadeDuration = 0.5f, float delay = 0f)
    {
        if (instructionBox == null) yield break;

        CanvasGroup canvasGroup = instructionBox.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogWarning("No CanvasGroup found on " + instructionBox.name);
            yield break;
        }

        yield return new WaitForSeconds(delay);

        instructionBox.SetActive(true);
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

    private IEnumerator HandleInstructions()
    {
        //monit skanowania powierzchni
        scanSurfaceInstruction.SetActive(true);
        yield return new WaitForSeconds(instructionDuration);

        float elapsedTime = 0f;
        while (elapsedTime < searchDuration) //szukanie powierzchni przez okreslony czas
        {
            if (placeObjectScript.placed || IsSurfaceDetected())
            {
                break;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (placeObjectScript.placed || IsSurfaceDetected())
        {
            yield return StartCoroutine(FadeOut(scanSurfaceInstruction));

            if (instructionsEnabled)
            {
                if (!placeObjectScript.placed)
                {
                    //monit "dotknij ekran zeby postawiæ obiekt"
                    yield return StartCoroutine(FadeIn(touchScreenInstruction));

                    //oczekiwanie na postawienie obiektu
                    while (!placeObjectScript.placed)
                    {
                        yield return null;
                    }                   
                }
                yield return StartCoroutine(FadeOut(touchScreenInstruction));


                //monit o skalowaniu powierzchni
                yield return StartCoroutine(FadeIn(scaleInstruction));
                yield return new WaitForSeconds(instructionDuration);
                yield return StartCoroutine(FadeOut(scaleInstruction));

                //monit dla iteratora
                string algorithm = PlayerPrefs.GetString("algorithm");
                if (algorithm.Contains("ListStruct"))
                {
                    yield return StartCoroutine(FadeIn(moveIteratorInstruction));
                    yield return new WaitForSeconds(instructionDuration);
                    yield return StartCoroutine(FadeOut(moveIteratorInstruction));
                }

            }
        }
        else //wyswietlenie bledu jesli nie znaleziono powierzchni
        {
            scanSurfaceInstruction.SetActive(false);
            scanErrorInstruction.SetActive(true); //wyœwietlenie komunikatu b³êdu
        }
    }

    


}
