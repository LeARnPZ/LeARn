using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ObjectScaler : MonoBehaviour
{
    private float initialDistance;
    private Vector3 initialScale;
    private float currentScale;

    private void OnEnable()
    {
        // Aktywacja obs³ugi dotyku i symulacji w edytorze
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        initialScale = transform.localScale;
        currentScale = 1;
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
            }
            else
            {
                float currentDistance = Vector2.Distance(touch1.screenPosition, touch2.screenPosition);
                if (Mathf.Approximately(initialDistance, 0)) return;

                float scaleFactor = currentScale * (currentDistance / initialDistance);
                if(scaleFactor < 1)
                {
                    currentScale = Mathf.Max(scaleFactor, (float)0.4);
                }
                else if(scaleFactor > 1)
                {
                    currentScale = Mathf.Min(scaleFactor, (float)2.5);
                }

                initialDistance = currentDistance;
                transform.localScale = initialScale * currentScale;

                Graphs graphs = GetComponent<Graphs>();
                if (graphs != null)
                {
                    graphs.UpdateEdgePositions();
                }
            }
        }
    }
}
