using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager), typeof(ARAnchorManager))]
public class PlaceObject : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private ARAnchorManager anchorManager;
    private List<ARRaycastHit> hits = new();

    private bool canPlace = false;

    private bool placed = false;
    private string algorithmName;
    public TextMeshProUGUI placementPromptText;
    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        anchorManager = GetComponent<ARAnchorManager>();
        algorithmName = PlayerPrefs.GetString("algorithm");
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

    bool delay_started = false;

    private IEnumerator DelayedPlacement()
    {
        delay_started = true;
        for (int i = 5; i > 0; i--)
        {
            placementPromptText.text = $"Place the object in {i}...";
            yield return new WaitForSeconds(1f);
        }

        placementPromptText.text = "Place the object!";
        canPlace = true;
    }


    private void FingerDown(EnhancedTouch.Finger finger)
    {
        
        int placementMode = PlayerPrefs.GetInt("placement", 0);

        if (placementMode == 0)
        
        {
        
            if (finger.index != 0 || placed) return;

            GameObject prefab = (GameObject) Resources.Load($"Animations/{algorithmName}");

            if (raycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                //foreach (ARRaycastHit hit in hits)
                //{
                //    Pose pose = hit.pose;
                //    Instantiate(prefab, pose.position, pose.rotation, GameObject.Find("Animation").transform);
                //}S
                Pose pose = hits[0].pose;
                    Instantiate(prefab, pose.position, pose.rotation, GameObject.Find("Animation").transform);
                placed = true;

                string algorithm = PlayerPrefs.GetString("algorithm");
                if (algorithm.Contains("Sort") || algorithm.Contains("Graph"))
                {
                    GameObject.Find("RestartButton").GetComponent<Button>().interactable = true;
                    GameObject.Find("PlayPauseButton").GetComponent<Button>().interactable = true;
                    GameObject.Find("SpeedButton").GetComponent<Button>().interactable = true;

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

        if (placementMode == 1)
        {
            if (placed) 
            {
                planeManager.enabled = false;
                return;
            }

            if (!canPlace && !delay_started)
            {
                StartCoroutine(DelayedPlacement());
                return;
            }

            // placementPromptText.text = "";
            // placementPromptText.enabled = false;

            GameObject prefab = (GameObject)Resources.Load($"Animations/{algorithmName}");
            if (prefab == null) return;

            Camera arCamera = Camera.main;
            Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * 1.5f;

            ARPlane bestPlane = null;
            float closestDistance = float.MaxValue;

            foreach (ARPlane plane in planeManager.trackables)
            {
                if (plane.trackingState == TrackingState.Tracking && plane.alignment == PlaneAlignment.HorizontalUp)
                {
                    float distance = Vector3.Distance(plane.center, spawnPosition);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        bestPlane = plane;
                    }
                }
            }

            GameObject spawnedObject;

            if (bestPlane != null && closestDistance < 3.0f)
            {
                Vector3 projectedPosition = ProjectPointOnPlane(bestPlane.transform, spawnPosition);
                spawnedObject = Instantiate(prefab, projectedPosition, Quaternion.identity);
            }
            else
            {
                ARAnchor anchor = null;

                if (anchorManager != null)
                {
                    anchor = anchorManager.AddAnchor(new Pose(spawnPosition, Quaternion.identity));
                }

                if (anchor != null)
                {
                    spawnedObject = Instantiate(prefab, anchor.transform.position, anchor.transform.rotation);
                }
                else
                {
                    spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
                }
            }

            spawnedObject.transform.SetParent(GameObject.Find("Animation").transform, worldPositionStays: true);

            Vector3 directionToCamera = arCamera.transform.position - spawnedObject.transform.position;
            directionToCamera.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(-directionToCamera); 
            spawnedObject.transform.rotation = lookRotation;

            float distanceToCamera = Vector3.Distance(arCamera.transform.position, spawnedObject.transform.position);
            float scaleFactor = Mathf.Clamp(distanceToCamera / 1.5f, 0.8f, 1.5f);
            spawnedObject.transform.localScale *= scaleFactor;

            placed = true;
            placementPromptText.text = "";

            string algorithm = PlayerPrefs.GetString("algorithm");
            if (algorithm.Contains("Sort") || algorithm.Contains("Graph"))
            {
                GameObject.Find("RestartButton").GetComponent<Button>().interactable = true;
                GameObject.Find("PlayPauseButton").GetComponent<Button>().interactable = true;
                GameObject.Find("SpeedButton").GetComponent<Button>().interactable = true;
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


    private Vector3 ProjectPointOnPlane(Transform planeTransform, Vector3 point)
    {
        Vector3 planeNormal = planeTransform.up;
        Vector3 planePoint = planeTransform.position;
        
        float distance = Vector3.Dot(planeNormal, point - planePoint);
        return point - planeNormal * distance;
    }

    

}
