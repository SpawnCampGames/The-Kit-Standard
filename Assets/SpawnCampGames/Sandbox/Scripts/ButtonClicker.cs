using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClicker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("button pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("button released");
    }
}
