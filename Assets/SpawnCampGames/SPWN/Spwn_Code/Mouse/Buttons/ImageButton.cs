using UnityEngine;

namespace SPWN
{

    /// <summary>
    /// Logs a message when the mouse enters the object's collider.
    /// </summary>
    public class ImageButton : MonoBehaviour
    {
        private void OnMouseEnter()
        {
            Debug.Log($"Hovering over: {gameObject.name}");
            // Add visual feedback for hovering (e.g., change color)
        }

        private void OnMouseExit()
        {
            Debug.Log($"Stopped hovering over: {gameObject.name}");
            // Revert visual feedback when not hovering
        }

        private void OnMouseDown()
        {
            Debug.Log($"Clicked on: {gameObject.name}");
            // Trigger button click event or action
        }

        private void OnMouseUp()
        {
            Debug.Log($"Released on: {gameObject.name}");
            // Optionally handle logic when the mouse button is released
        }
    }

}
