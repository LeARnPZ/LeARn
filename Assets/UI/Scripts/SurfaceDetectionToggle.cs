using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARSurfaceVisibilityController : MonoBehaviour
{
    public ARPlaneManager arPlaneManager;
    public Slider visibilitySlider;
    private bool isVisible;

    void Start()
    {
        if (arPlaneManager == null)
            return;

        if (PlayerPrefs.GetInt("PoorMode") == 1)
            AutoDisableSlider();

        if (PlayerPrefs.HasKey("PlaneVisibility"))
        {
            isVisible = PlayerPrefs.GetInt("PlaneVisibility") == 1;
        }
        else
        {
            isVisible = true;
            PlayerPrefs.SetInt("PlaneVisibility", 1);
            PlayerPrefs.Save();
        }

        visibilitySlider.value = isVisible ? 1f : 0f;

        arPlaneManager.planesChanged += OnPlanesChanged;
        SetARPlaneVisibility(isVisible);
    }

    public void OnSliderValueChanged(float value)
    {
        isVisible = value == 1f;

        SetARPlaneVisibility(isVisible);
        PlayerPrefs.SetInt("PlaneVisibility", isVisible ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void SetARPlaneVisibility(bool visible)
    {
        arPlaneManager.planePrefab.SetActive(visible);
        foreach (var plane in arPlaneManager.trackables)
            plane.gameObject.SetActive(visible);
    }

    public void AutoDisableSlider()
    {
        bool poorMode = PlayerPrefs.GetInt("PoorMode") == 1;
        if (poorMode && visibilitySlider != null)
        {
            visibilitySlider.value = 0f;
            visibilitySlider.interactable = false;
            PlayerPrefs.SetInt("PlaneVisibility", 0);
            PlayerPrefs.Save();
            GameObject.FindAnyObjectByType<SliderHandleClickListener>(FindObjectsInactive.Include).enabled = false;
        }
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (var addedPlane in args.added)
            addedPlane.gameObject.SetActive(isVisible);

        foreach (var removedPlane in args.removed)
            removedPlane.gameObject.SetActive(isVisible);

        foreach (var updatedPlane in args.updated)
            updatedPlane.gameObject.SetActive(isVisible);
    }

    void OnDisable()
    {
        if (arPlaneManager != null)
            arPlaneManager.planesChanged -= OnPlanesChanged;
    }
}
