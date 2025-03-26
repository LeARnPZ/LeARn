using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARSurfaceVisibilityController : MonoBehaviour
{
    public ARPlaneManager arPlaneManager;
    public Slider visibilitySlider;
    private bool isVisible = true;

    void Start()
    {
        if (arPlaneManager == null) return;

        SetARPlaneVisibility(isVisible);

        if (visibilitySlider != null)
        {
            visibilitySlider.onValueChanged.AddListener(OnSliderValueChanged);
            visibilitySlider.value = 1;
        }
        arPlaneManager.planesChanged += OnPlanesChanged;
    }

    private void OnSliderValueChanged(float value)
    {
        isVisible = value == 1;
        SetARPlaneVisibility(isVisible);
    }

    private void SetARPlaneVisibility(bool visible)
    {
        arPlaneManager.planePrefab.SetActive(visible);
        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(visible);
        }

    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (var addedPlane in args.added)
        {
            addedPlane.gameObject.SetActive(isVisible);

        }

        foreach (var removedPlane in args.removed)
        {
            removedPlane.gameObject.SetActive(isVisible);
        }

        foreach (var updatedPlane in args.updated)
        {
            updatedPlane.gameObject.SetActive(isVisible);
        }
    }

    void OnDisable()
    {
        if (arPlaneManager != null)
        {
            arPlaneManager.planesChanged -= OnPlanesChanged;
        }
    }
}
