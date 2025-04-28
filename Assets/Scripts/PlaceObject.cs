using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class PlaceObject : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private List<ARRaycastHit> hits = new();

    private ARAnchorManager anchorManager;

    public bool placed = false;
    private bool poorMode;
    private string algorithmName;
    private readonly float distanceFromCamera = 0.75f;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        anchorManager = GetComponent<ARAnchorManager>();

        algorithmName = PlayerPrefs.GetString("algorithm");
        poorMode = PlayerPrefs.GetInt("PoorMode") == 1; 
    }

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.TouchSimulation.Disable();
    }

    private void FingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0 || placed) return;

        if (IsTouchOverUI(finger.currentTouch.screenPosition)) return;

        GameObject prefab = (GameObject) Resources.Load($"Animations/{algorithmName}");
        GameObject animationObject = GameObject.Find("Animation");

        if (poorMode)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Camera cam = Camera.main;
                Vector3 position = cam.transform.position + cam.transform.forward * distanceFromCamera;
                Quaternion rotation = Quaternion.LookRotation(cam.transform.position - position);

                Pose pose = new(position, rotation);
                ARAnchor anchor = anchorManager.AddAnchor(pose);
                if (anchor == null)
                {
                    Debug.LogWarning("Nie udało się dodać anchor'a!");
                    return;
                }

                animationObject.transform.SetParent(anchor.transform, worldPositionStays: true);
                animationObject.transform.localPosition = Vector3.zero;
                Instantiate(prefab, pose.position, Quaternion.identity, animationObject.transform);
                animationObject.transform.GetChild(0).localScale = Vector3.one / 20f;
                placed = true;
            }
        }
        else
        {
            if (raycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose pose = hits[0].pose;
                ARAnchor anchor = anchorManager.AddAnchor(pose);
                if (anchor == null)
                {
                    Debug.LogWarning("Nie udało się dodać anchor'a!");
                    return;
                }

                animationObject.transform.SetParent(anchor.transform, worldPositionStays: true);
                Instantiate(prefab, pose.position, pose.rotation, animationObject.transform);
                placed = true;
            }
        }

        if (placed)
        {
            string algorithm = PlayerPrefs.GetString("algorithm");
            if (algorithm.Contains("Sort") || algorithm.Contains("Graph"))
            {
                GameObject.Find("RestartButton").GetComponent<Button>().interactable = true;
                GameObject.Find("PlayPauseButton").GetComponent<Button>().interactable = true;
                GameObject.Find("SpeedButton").GetComponent<Button>().interactable = true;
                if (algorithm.Contains("Dijkstra"))
                    GameObject.Find("DijkstraDropdownContainer").GetComponent<DijkstraDropdownController>().DropdownSetup();
            }
            else if (algorithm.Contains("StackStruct"))
            {
                GameObject.Find("BottomButtons/StructButtonsStack/AddItemButton").GetComponent<Button>().interactable = true;
                GameObject.Find("BottomButtons/StructButtonsStack/PopItemButton").GetComponent<Button>().interactable = true;
                GameObject.Find("BottomButtons/StructButtonsStack/PeekItemButton").GetComponent<Button>().interactable = true;

            }
            else if (algorithm.Contains("QueueStruct"))
            {
                GameObject.Find("BottomButtons/StructButtonsQueue/AddItemButton").GetComponent<Button>().interactable = true;
                GameObject.Find("BottomButtons/StructButtonsQueue/PopItemButton").GetComponent<Button>().interactable = true;
                GameObject.Find("BottomButtons/StructButtonsQueue/PeekItemButton").GetComponent<Button>().interactable = true;
            }
            else if (algorithm.Contains("ListStruct"))
            {
                GameObject.Find("BottomButtons/StructButtonsList/AddItemButton").GetComponent<Button>().interactable = true;
                GameObject.Find("BottomButtons/StructButtonsList/PopItemButton").GetComponent<Button>().interactable = true;
                GameObject.Find("BottomButtons/StructButtonsList/PeekItemButton").GetComponent<Button>().interactable = true;
            }
        }
    }

    private bool IsTouchOverUI(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = touchPosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }
    
    public void ActivatePoorMode()
    {
        poorMode = true;

        PlayerPrefs.SetInt("PoorMode", 1);
        PlayerPrefs.Save();

        FindAnyObjectByType<ARSurfaceVisibilityController>().AutoDisableSlider();
    }
}
