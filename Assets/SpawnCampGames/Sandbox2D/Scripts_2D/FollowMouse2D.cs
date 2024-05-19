using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float distanceFromCamera = 10f; // Distance from the camera to place the object

    private void Awake() {
        Cursor.visible = false;
    }

    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Calculate the distance from the camera to the object in world space
        float distanceToPlane;
        if (Camera.main.orthographic)
        {
            // For orthographic cameras, use the camera's orthographic size
            distanceToPlane = Camera.main.orthographicSize;
        }
        else
        {
            // For perspective cameras, calculate the distance based on the camera's field of view
            distanceToPlane = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2f) * distanceFromCamera;
        }

        // Convert the screen position to a point in the game's world space
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, distanceToPlane));

        // Update the GameObject's position to follow the mouse
        transform.position = mouseWorldPosition;
    }
}