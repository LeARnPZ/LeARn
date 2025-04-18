using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager), typeof(ARAnchorManager))]
public class PlaceObject : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private ARAnchorManager anchorManager;
    private List<ARRaycastHit> hits = new();
    private bool placed = false;
    private string algorithmName;
    private ARAnchor spawnedAnchor;
    private GameObject spawnedObject;

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


    private void FingerDown(EnhancedTouch.Finger finger)
    {
        int placementMode = PlayerPrefs.GetInt("placement", 0);

        if (finger.index != 0 || placed) return;

        GameObject prefab = (GameObject)Resources.Load($"Animations/{algorithmName}");
        
        if (prefab == null) return;

        if (placementMode == 0)
        {
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
        else if (placementMode == 1)
        {
            Camera arCamera = Camera.main;
            Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * 1.5f;
            
            ARPlane bestPlane = FindClosestPlane(spawnPosition);
            
            if (bestPlane != null)
            {
                Vector3 projectedPosition = ProjectPointOnPlane(bestPlane.transform, spawnPosition);
                Pose anchorPose = new Pose(projectedPosition, Quaternion.identity);
                
                spawnedAnchor = anchorManager.AttachAnchor(bestPlane, anchorPose);
                
                if (spawnedAnchor != null)
                {
                    spawnedObject = Instantiate(prefab, spawnedAnchor.transform);
                    placed = true;
                }
                else
                {
                    // Fallback if anchor creation fails
                    spawnedObject = Instantiate(prefab, projectedPosition, Quaternion.identity);
                    placed = true;
                }
            }
            else
            {
                Pose anchorPose = new Pose(spawnPosition, Quaternion.identity);
                spawnedAnchor = anchorManager.AddAnchor(anchorPose);
                
                if (spawnedAnchor != null)
                {
                    spawnedObject = Instantiate(prefab, spawnedAnchor.transform);
                    placed = true;
                }
                else
                {
                    spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
                    placed = true;
                }
            }

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
    
    private ARPlane FindClosestPlane(Vector3 position)
    {
        ARPlane bestPlane = null;
        float closestDistance = 1.5f;
        
        foreach (ARPlane plane in planeManager.trackables)
        {
            if (plane.trackingState == TrackingState.Tracking && plane.alignment == PlaneAlignment.HorizontalUp)
            {
                float distance = Vector3.Distance(plane.center, position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    bestPlane = plane;
                }
            }
        }
        
        return bestPlane;
    }

    private Vector3 ProjectPointOnPlane(Transform planeTransform, Vector3 point)
    {
        Vector3 planeNormal = planeTransform.up;
        Vector3 planePoint = planeTransform.position;
        
        float distance = Vector3.Dot(planeNormal, point - planePoint);
        return point - planeNormal * distance;
    }
    
    public void ResetPlacement()
    {
        if (spawnedAnchor != null)
        {
            Destroy(spawnedAnchor.gameObject);
            spawnedAnchor = null;
        }
        
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            spawnedObject = null;
        }
        
        placed = false;
    }
}