using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClicker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    bool isHovering;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        Debug.Log($"Hovering over: {gameObject.name}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isHovering) // redundant
        {
            Debug.Log($"Clicked on: {gameObject.name}");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
