using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.HID;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class MovePlacedObject : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new();

    private GameObject selectedObject;
    private bool isDragging = false;
    private ARAnchor currentAnchor;

    [SerializeField]
    GameObject ParentAnimation;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            OnTouchBegan(touch);
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            OnTouchMoved(touch);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            OnTouchEnded();
        }
    }

    private void OnTouchBegan(Touch touch)
    {
        if (IsTouchOverUI(touch.position)) return;

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform != null && hit.transform.IsChildOf(ParentAnimation.transform))
            {
                selectedObject = hit.transform.root.gameObject;
                isDragging = true;


            }
        }
    }

    private void OnTouchMoved(Touch touch)
    {
        if (!isDragging || selectedObject == null) return;

        
        ARAnchor anchor = ParentAnimation.transform.parent.GetComponent<ARAnchor>();

         if (anchor != null)
         {
             currentAnchor = anchor;
             selectedObject.transform.SetParent(null);  
         }

        
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;
            selectedObject.transform.position = pose.position;

            anchor.transform.position = pose.position;
            selectedObject.transform.SetParent(currentAnchor.transform, worldPositionStays: true);

        }
    }

    private void OnTouchEnded()
    {
        isDragging = false;
        selectedObject = null;
    }

    private bool IsTouchOverUI(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = touchPosition;

        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }
}
