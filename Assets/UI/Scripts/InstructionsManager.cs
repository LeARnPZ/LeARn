using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using System.Linq;
using TMPro;


public class InstructionsManager : MonoBehaviour
{
    [Header("Instructions Images")]
    public GameObject scanSurfaceInstruction;
    public GameObject scanErrorInstruction;
    public GameObject touchScreenInstruction;
    public GameObject scaleInstruction;
    public GameObject moveIteratorInstruction;
    public GameObject moveObjectInstruction;

    [Header("Other")]
    public ARPlaneManager planeManager;
    public PlaceObject placeObjectScript;
    public GameObject infoPopupWindow;
    public GameObject infoPopupWindowStructs;

    [Header("Settings")]
    public float minPlaneSize = 0.3f;
    public float instructionDuration = 1.5f;
    public float searchDuration = 20f;

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
        string algorithm = PlayerPrefs.GetString("algorithm");
        bool poorMode = PlayerPrefs.GetInt("PoorMode") == 1;
        GameObject currentInfoPopup = algorithm.Contains("Struct") ? infoPopupWindowStructs : infoPopupWindow;

        //monit skanowania powierzchni
        if (poorMode)
            scanSurfaceInstruction.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Poruszaj telefonem, aby zeskanowaæ otoczenie";
        scanSurfaceInstruction.SetActive(true);
        yield return new WaitForSeconds(instructionDuration);

        float elapsedTime = 0f;
        while (elapsedTime < searchDuration) //szukanie powierzchni przez okreslony czas
        {

            // jesli okienko z info o algo zamkniete
            if (placeObjectScript.placed || IsSurfaceDetected())
            {
                break;
            }

            // jesli okienko z info o algo jest otwarte, wstrzymaj odliczanie
            if (currentInfoPopup.activeSelf)
            {
                yield return null;
                continue;
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
                    touchScreenInstruction.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Dotknij ekranu, aby umieœciæ animacjê";
                    yield return StartCoroutine(FadeIn(touchScreenInstruction));

                    //oczekiwanie na postawienie obiektu
                    while (!placeObjectScript.placed)
                    {
                        yield return null;
                    }                   
                }
                yield return StartCoroutine(FadeOut(touchScreenInstruction));


                //monit o skalowaniu obiektu
                yield return StartCoroutine(FadeIn(scaleInstruction));
                yield return new WaitForSeconds(instructionDuration+0.5f);
                yield return StartCoroutine(FadeOut(scaleInstruction));

                //monit o przesuwaniu obiektu
                yield return StartCoroutine(FadeIn(moveObjectInstruction));
                yield return new WaitForSeconds(instructionDuration + 0.5f);
                yield return StartCoroutine(FadeOut(moveObjectInstruction));

                //monit dla iteratora
                if (algorithm.Contains("ListStruct"))
                {
                    yield return StartCoroutine(FadeIn(moveIteratorInstruction));
                    yield return new WaitForSeconds(instructionDuration+1.5f);
                    yield return StartCoroutine(FadeOut(moveIteratorInstruction));
                }

            }
        }
        else //wyswietlenie bledu jesli nie znaleziono powierzchni
        {
            if (poorMode)
            {
                StartCoroutine(FadeOut(scanSurfaceInstruction));
            }
            else
            {
                scanSurfaceInstruction.SetActive(false);
                scanErrorInstruction.SetActive(true); //wyœwietlenie komunikatu b³êdu

                placeObjectScript.ActivatePoorMode();
                yield return new WaitForSeconds(instructionDuration);
                StartCoroutine(FadeOut(scanErrorInstruction));
            }
        }
    }

    


}
