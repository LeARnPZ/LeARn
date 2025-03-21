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

    private GameObject spawnedObject;

    private void FingerDown(EnhancedTouch.Finger finger)
    {
        GameObject prefab = (GameObject)Resources.Load($"Animations/{algorithmName}");

        if (prefab == null)
        {
            Debug.LogError($"Prefab for {algorithmName} not found!");
            return;
        }

        Camera arCamera = Camera.main;
        Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * 1f;

        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity, GameObject.Find("Animation").transform);
        }
        else
        {
            spawnedObject.transform.position = spawnPosition;
        }
    }

}