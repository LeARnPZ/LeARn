using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOrientationManager : MonoBehaviour
{

    public GameObject portraitCanvas;
    public GameObject landscapeCanvas;
    public GameObject mainCanvas;

    public ScreenOrientation _lastOrientation;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            _lastOrientation = Screen.orientation;
            UpdateCanvasForOrientation(_lastOrientation);
            StartCoroutine(CheckOrientationChange());
        }
    }

    private IEnumerator CheckOrientationChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            if (Screen.orientation != _lastOrientation)
            {
                _lastOrientation = Screen.orientation;
                UpdateCanvasForOrientation(_lastOrientation);
            }
        }
    }

    private void UpdateCanvasForOrientation(ScreenOrientation orientation)
    {
        GameObject referenceCanvas = (orientation == ScreenOrientation.Portrait) ? portraitCanvas : landscapeCanvas;

        CopyLayout(referenceCanvas.transform, mainCanvas.transform);
    }

    private void CopyLayout(Transform from, Transform to)
    {
        for (int i = 0; i < from.childCount; i++)
        {
            Transform fromChild = from.GetChild(i);
            Transform toChild = to.Find(fromChild.name);

            if (toChild != null)
            {
                // Rekurencja
                CopyLayout(fromChild, toChild);

                RectTransform fromRect = fromChild.GetComponent<RectTransform>();
                RectTransform toRect = toChild.GetComponent<RectTransform>();

                if (fromRect != null && toRect != null)
                {
                    toRect.anchoredPosition = fromRect.anchoredPosition;
                    toRect.sizeDelta = fromRect.sizeDelta;
                    toRect.anchorMin = fromRect.anchorMin;
                    toRect.anchorMax = fromRect.anchorMax;
                    toRect.pivot = fromRect.pivot;
                    toRect.localScale = fromRect.localScale;
                    toRect.localRotation = fromRect.localRotation;
                }
            }
        }
    }

}
