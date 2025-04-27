using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARSurfaceVisibilityController : MonoBehaviour
{
    public ARPlaneManager arPlaneManager;
    public Slider visibilitySlider;
    private bool isVisible;
    private bool allowSliderChanges = true;

    void Start()
    {
        if (arPlaneManager == null) return;

        bool poorMode = PlayerPrefs.GetInt("PoorMode") == 1;
        if (poorMode && visibilitySlider != null)
        {
            visibilitySlider.interactable = false;
            visibilitySlider.value = 0f;
            isVisible = false;
            SetARPlaneVisibility(false);
            PlayerPrefs.SetInt("PlaneVisibility", 0);
            PlayerPrefs.Save();
            visibilitySlider.enabled = false;

            allowSliderChanges = false;
            return;
        }

        if (PlayerPrefs.HasKey("PlaneVisibility"))
        {
            if(PlayerPrefs.GetInt("PlaneVisibility") == 1)
            {
                isVisible = true;
            }
            else if(PlayerPrefs.GetInt("PlaneVisibility") == 0)
            {
                isVisible = false;
            }
        }
        else
        {
            isVisible = true;
            PlayerPrefs.SetInt("PlaneVisibility", 1);
            PlayerPrefs.Save();
        }


        if (isVisible)
        {
            visibilitySlider.value = 1f;
        }
        else
        {
            visibilitySlider.value = 0f;
        }

        arPlaneManager.planesChanged += OnPlanesChanged;
        SetARPlaneVisibility(isVisible);
    }

    public void OnSliderValueChanged(float value)
    {

        if (!allowSliderChanges)
            return;

        if(value == 1f)
        {
            isVisible = true;
        }
        else
        {
            isVisible = false;
        }

        SetARPlaneVisibility(isVisible);
        PlayerPrefs.SetInt("PlaneVisibility", isVisible ? 1 : 0);
        PlayerPrefs.Save();
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
