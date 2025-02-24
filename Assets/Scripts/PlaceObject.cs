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
        if (finger.index != 0 || placed) return;

        GameObject prefab = (GameObject) Resources.Load($"Animations/{algorithmName}");

        if (raycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            //foreach (ARRaycastHit hit in hits)
            //{
            //    Pose pose = hit.pose;
            //    Instantiate(prefab, pose.position, pose.rotation, GameObject.Find("Animation").transform);
            //}
            Pose pose = hits[0].pose;
                Instantiate(prefab, pose.position, pose.rotation, GameObject.Find("Animation").transform);
            placed = true;

            string algorithm = PlayerPrefs.GetString("algorithm");
            if (algorithm.Contains("Sort"))
            {
                GameObject.Find("RestartButton").GetComponent<Button>().interactable = true;
                GameObject.Find("PlayPauseButton").GetComponent<Button>().interactable = true;
            }
            else if (algorithm.Contains("Struct"))
            {
                GameObject.Find("AddItemButton").GetComponent<Button>().interactable = true;
                GameObject.Find("PopItemButton").GetComponent<Button>().interactable = true;
            }
        }

    }
}
