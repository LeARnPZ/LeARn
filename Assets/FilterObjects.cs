using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

public class FilterCategories : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public GameObject contentContainer;
    public GameObject emptyObject;
    public ScrollRect scrollRect;
    private string defaultToggleName = "All";
    private Toggle lastSelectedToggle;

    private void Start()
    {
        //dodanie listenera na kazdy toggler
        foreach (Toggle toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.AddListener(_ => FilterObjects());
        }

        //ustawienie defaultowo zaznaczonego togglera na toggler 'Wszystko'
        Toggle defaultToggle = toggleGroup.GetComponentsInChildren<Toggle>().FirstOrDefault(t => t.name == defaultToggleName);
        if (defaultToggle == null) return;
        defaultToggle.isOn = true;

        
    }

    private void FilterObjects()
    {
        Toggle selectedToggle = toggleGroup.ActiveToggles().FirstOrDefault();
        if (selectedToggle == lastSelectedToggle) return;
        lastSelectedToggle = selectedToggle;

        string selectedTag = selectedToggle.name;

        foreach (Transform item in contentContainer.transform)
        {
            bool shouldBeVisible;
            if (selectedTag == defaultToggleName)
            {
                shouldBeVisible = true;
                
            }
            else
            {
                shouldBeVisible = item.gameObject.CompareTag(selectedTag);
            }

            item.gameObject.SetActive(shouldBeVisible);
            
        }


        if (emptyObject != null)
        {
            emptyObject.SetActive(selectedTag == "Structures");
        }

        if (scrollRect != null)
        {
            StartCoroutine(ScrollToTopWithDelay());
            scrollRect.verticalNormalizedPosition = 1f;

        }

    }

    private IEnumerator FadeInAnimation()
    {
        //ladne ladowanie poczatkowego ekranu
        CanvasGroup canvasGroup = contentContainer.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = contentContainer.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;

        float duration = 0.8f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    private IEnumerator ScrollToTopWithDelay()
    { 
        scrollRect.enabled = false;
  
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentContainer.GetComponent<RectTransform>());
        yield return null;

        scrollRect.verticalNormalizedPosition = 1f;
        scrollRect.enabled = true;
        
    }


}
