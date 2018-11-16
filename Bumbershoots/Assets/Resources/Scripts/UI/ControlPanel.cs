using UnityEngine;
using UnityEngine.EventSystems;

public class ControlPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void FingerDownHandler(); 
    
    public event FingerDownHandler FingerDown;

    public delegate void FingerUpHandler();

    public event FingerUpHandler FingerUp;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (FingerDown != null)
            FingerDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (FingerUp != null)
            FingerUp();
    }
}
