using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneOrientationManager : MonoBehaviour
{
    
    public GameObject portraitCanvas;
    public GameObject landscapeCanvas;
    public GameObject mainCanvas;
    [SerializeField]
    private GameObject animationObject;

    private ScreenOrientation _lastOrientation;

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
            Screen.autorotateToPortraitUpsideDown = false;
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
        }
    }

   
    void Update()
    {
        if (Screen.orientation != _lastOrientation)
        {
            _lastOrientation = Screen.orientation;
            UpdateCanvasForOrientation(_lastOrientation);
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
                
                // Obs³uga zmiany z grupowania vertical-horizontal
                if (fromChild.gameObject.name == "InfoOptionsButtonContainer")
                {
                    var existingVertical = toChild.GetComponent<VerticalLayoutGroup>();
                    var existingHorizontal = toChild.GetComponent<HorizontalLayoutGroup>();


                    if (existingVertical) Destroy(existingVertical);
                    if (existingHorizontal) Destroy(existingHorizontal);

                    var fromVertical = from.GetComponent<VerticalLayoutGroup>();
                    var fromHorizontal = from.GetComponent<HorizontalLayoutGroup>();

                    if (fromVertical)
                    {
                        var copy = to.gameObject.AddComponent<VerticalLayoutGroup>();
                        CopyLayoutGroupSettings(copy, fromVertical);
                    }
                    else if (fromHorizontal)
                    {
                        var copy = to.gameObject.AddComponent<HorizontalLayoutGroup>();
                        CopyLayoutGroupSettings(copy, fromHorizontal);
                    }

                }

                //kopiowanie wartosci wlasnosci w layoutach
                if (fromChild.gameObject.name == "BottomButtons" || fromChild.gameObject.name == "LeftSide" || fromChild.gameObject.name == "Top" || fromChild.gameObject.name == "RightSide" || fromChild.gameObject.name == "BottomButtonsSteps")
                {
                    var fromVertical = fromChild.GetComponent<VerticalLayoutGroup>();
                    var fromHorizontal = fromChild.GetComponent<HorizontalLayoutGroup>();
                    var fromLayout = fromVertical != null ? (HorizontalOrVerticalLayoutGroup) fromVertical : fromHorizontal;

                    var existingVertical = toChild.GetComponent<VerticalLayoutGroup>();
                    var existingHorizontal = toChild.GetComponent<HorizontalLayoutGroup>();
                    var existingLayout = existingVertical != null ? (HorizontalOrVerticalLayoutGroup) existingVertical : existingHorizontal;

                    if (fromLayout != null && existingLayout != null)
                    {
                        CopyLayoutGroupSettings(existingLayout, fromLayout);
                    }
                }

            }

                // Zmiana odpowiedniej czcionki
                if (fromChild.gameObject.name == "InfoText" ||
                    fromChild.gameObject.name == "ComplexityText" ||
                    fromChild.gameObject.name == "ComplexityValue" ||
                    fromChild.gameObject.name == "Text (TMP)")
                {
                    var fromText = fromChild.GetComponent<TMPro.TextMeshProUGUI>();
                    var toText = toChild.GetComponent<TMPro.TextMeshProUGUI>();

                    if (fromText != null && toText != null)
                    {
                        toText.enableAutoSizing = fromText.enableAutoSizing;
                        toText.fontSizeMin = fromText.fontSizeMin;
                        toText.fontSizeMax = fromText.fontSizeMax;
                        toText.characterSpacing = fromText.characterSpacing;
                    }

                }


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

    void CopyLayoutGroupSettings(HorizontalOrVerticalLayoutGroup target, HorizontalOrVerticalLayoutGroup source)
    {
        target.spacing = source.spacing;
        target.childAlignment = source.childAlignment;
        target.childForceExpandHeight = source.childForceExpandHeight;
        target.childForceExpandWidth = source.childForceExpandWidth;
        target.childControlHeight = source.childControlHeight;
        target.childControlWidth = source.childControlWidth;
        target.padding = source.padding;
    }


}
