using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;


[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class PoorPlaceObject : MonoBehaviour
{
    [SerializeField]

    private ARAnchorManager anchorManager;
    private bool placed = false;
    private string algorithmName;

    private void Awake()
    {
        algorithmName = PlayerPrefs.GetString("algorithm");
    }

    //private void OnEnable()
    //{
    //    EnhancedTouch.TouchSimulation.Enable();
    //    EnhancedTouch.EnhancedTouchSupport.Enable();
    //    EnhancedTouch.Touch.onFingerDown += FingerDown;
    //}

    //private void OnDisable()
    //{
    //    EnhancedTouch.Touch.onFingerDown -= FingerDown;
    //    EnhancedTouch.EnhancedTouchSupport.Disable();
    //    EnhancedTouch.TouchSimulation.Disable();
    //}

    private void Start()
    {
        anchorManager = GetComponent<ARAnchorManager>();
    }

    private void Update()
    {
        if (placed)
            return;

        
    }

}
