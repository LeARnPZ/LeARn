using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ObjectScaler : MonoBehaviour
{
    private float initialDistance;
    private Vector3 initialScale;

    private void OnEnable()
    {
        // Aktywacja obs³ugi dotyku i symulacji w edytorze
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        // Dezaktywacja obs³ugi dotyku i symulacji w edytorze
        EnhancedTouchSupport.Disable();
        TouchSimulation.Disable();
    }

    private void Update()
    {
        
        if (Touch.activeTouches.Count == 2)
        {
            var touch1 = Touch.activeTouches[0];
            var touch2 = Touch.activeTouches[1];

            if (touch1.phase == UnityEngine.InputSystem.TouchPhase.Began || touch2.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch1.screenPosition, touch2.screenPosition);
                initialScale = transform.localScale;
            }
            else
            {
                float currentDistance = Vector2.Distance(touch1.screenPosition, touch2.screenPosition);
                if (Mathf.Approximately(initialDistance, 0)) return;

                float scaleFactor = currentDistance / initialDistance;
                transform.localScale = initialScale * scaleFactor;

                Graphs graphs = GetComponent<Graphs>();
                if (graphs != null)
                {
                    graphs.UpdateEdgePositions();
                }
            }
        }
    }

    private void UpdateLineWidths()
    {
        LineRenderer[] lines = GetComponentsInChildren<LineRenderer>();

        foreach (var lr in lines)
        {
            float width = 0.1f * transform.localScale.x; // Base width scaled
            lr.startWidth = lr.endWidth = width;
        }
    }
}
