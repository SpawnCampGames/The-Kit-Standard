using UnityEngine;

namespace SPWN
{

    /// <summary>
    /// Logs a message when the mouse enters the object's collider.
    /// </summary>
    public class Sprite2DButton : MonoBehaviour
    {
        private void OnMouseEnter()
        {
            Debug.Log($"Hovering over: {gameObject.name}");
        }

        private void OnMouseExit()
        {
            Debug.Log($"Stopped hovering over: {gameObject.name}");
        }

        private void OnMouseDown()
        {
            Debug.Log($"Clicked on: {gameObject.name}");
        }

        private void OnMouseUp()
        {
            Debug.Log($"Released on: {gameObject.name}");
        }
    }

}
