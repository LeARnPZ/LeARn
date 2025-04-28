using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CloseUIOnTouch : MonoBehaviour
{
    public GameObject uiPanel;
    public GameObject exceptionButton;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (IsPointerOverSpecificUI(Input.GetTouch(0).position))
            {
            }
            else
            {
                uiPanel.SetActive(false);
            }
        }
    }

    private bool IsPointerOverSpecificUI(Vector2 screenPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == uiPanel || result.gameObject.transform.IsChildOf(uiPanel.transform) || (exceptionButton != null && result.gameObject == exceptionButton))
            {
                return true;
            }
        }

        return false;
    }
}
