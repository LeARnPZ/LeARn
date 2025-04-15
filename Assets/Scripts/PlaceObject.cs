using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;


[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class PlaceObject : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private List<ARRaycastHit> hits = new();

    private bool placed = false;
    private string algorithmName;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();

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
            if (finger.index != 0 || placed) return;

            GameObject prefab = (GameObject)Resources.Load($"Animations/{algorithmName}");

            if (prefab == null)
            {
                Debug.LogError($"Prefab for {algorithmName} not found!");
                return;
            }

            Camera arCamera = Camera.main;
            Vector2 screenPosition = finger.currentTouch.screenPosition;

            Ray ray = arCamera.ScreenPointToRay(screenPosition);
            Vector3 spawnPosition = ray.GetPoint(1f);

            GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity, GameObject.Find("Animation").transform);
            // spawnedObject.transform.LookAt(arCamera.transform);

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

}
