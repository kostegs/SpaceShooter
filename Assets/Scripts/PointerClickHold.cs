using UnityEngine;
using UnityEngine.EventSystems;

public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsHold { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsHold = true;    
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsHold = false;
    }
}
