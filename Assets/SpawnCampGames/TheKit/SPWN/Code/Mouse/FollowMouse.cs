using SPWN;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public enum FollowMode { Mode2D, Mode3D, ModeUI }
    public enum Plane2D { XY, XZ }

    [Tooltip("Select the follow mode for the object.")]
    public FollowMode followMode = FollowMode.Mode3D;

    [Header("General Settings")]
    [Tooltip("Distance from the camera to place the object (for 2D and 3D modes).")]
    public float distanceFromCamera = 10f;

    [Header("2D Mode Settings")]
    [Tooltip("Choose which 2D plane to follow: XY or XZ.")]
    public Plane2D plane2D = Plane2D.XY;

    [Header("3D Mode Settings")]
    [Tooltip("Offset to apply to the raycast hit position (for 3D mode).")]
    public float raycastOffset;
    [Tooltip("Orient the object to the normal of the surface hit by the raycast (for 3D mode).")]
    public bool useNormal;

    [Header("Cursor Settings")]
    [Tooltip("Hide the cursor when the object is following the mouse.")]
    public bool hideCursor;
    [Tooltip("Lock the cursor to the center of the screen.")]
    public bool lockCursor;

    [Header("Remap Settings")]
    [Tooltip("Minimum value for the remapped coordinates.")]
    public Vector2 min = new Vector2(-10, -10);
    [Tooltip("Maximum value for the remapped coordinates.")]
    public Vector2 max = new Vector2(10, 10);
    [Tooltip("Enable or disable remapping of coordinates.")]
    public bool remapCoords = true;

    [Header("Debug Info")]
    [Tooltip("Remapped coordinates for debug view.")]
    public Vector2 remappedCoords;
    private RectTransform uiElement;

    private void Awake()
    {
      //  Cursor.visible = !hideCursor;
      //  Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;

        if (followMode == FollowMode.ModeUI)
        {
            uiElement = GetComponent<RectTransform>();
            if (uiElement == null)
            {
                Debug.LogWarning("UI Follow Mode requires a RectTransform component.");
            }
        }
    }

    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;

        if (Cursor.lockState != CursorLockMode.Locked)
        {
            switch (followMode)
            {
                case FollowMode.Mode3D:
                    FollowIn3D(mouseScreenPosition);
                    break;
                case FollowMode.Mode2D:
                    FollowIn2D(mouseScreenPosition);
                    break;
                case FollowMode.ModeUI:
                    FollowInUI(mouseScreenPosition);
                    break;
            }

            Vector2 normalizedCoords = GetNormalizedScreenCoordinates(mouseScreenPosition);

            remappedCoords = remapCoords ? RemapCoordinates(normalizedCoords, min, max) : normalizedCoords;
            remappedCoords = new Vector2(Mathf.Clamp(remappedCoords.x, min.x, max.x), Mathf.Clamp(remappedCoords.y, min.y, max.y));
        }
        else
        {
            // cursor is locked, reset position
            ResetPosition();
        }
    }

    private void FollowIn3D(Vector3 mouseScreenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
        if(Physics.Raycast(ray,out RaycastHit hit))
        {
            Vector3 hitPosition = hit.point + Vector3.forward * raycastOffset;
            transform.position = hitPosition;

            if(useNormal)
            {
                transform.up = hit.normal;
            }
        }
    }

    private void FollowIn2D(Vector3 mouseScreenPosition)
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, distanceFromCamera));

        if (plane2D == Plane2D.XY)
            transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y,mouseWorldPosition.z);  // X-Y plane
        else
            transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, mouseWorldPosition.z);  // X-Z plane
    }

    private void FollowInUI(Vector3 mouseScreenPosition)
    {
        if (uiElement != null)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(uiElement.parent as RectTransform, mouseScreenPosition, null, out localPoint);
            uiElement.localPosition = localPoint;
        }
    }

    private void ResetPosition()
    {
        switch (followMode)
        {
            case FollowMode.Mode3D:
                transform.position = Vector3.zero;
                break;
            case FollowMode.Mode2D:
                transform.position = new Vector3(0, 0, distanceFromCamera);
                break;
            case FollowMode.ModeUI:
                if (uiElement != null) uiElement.localPosition = Vector2.zero;
                break;
        }
    }

    private Vector2 GetNormalizedScreenCoordinates(Vector3 screenPosition)
    {
        float x = (screenPosition.x / Screen.width) * 2 - 1;
        float y = (screenPosition.y / Screen.height) * 2 - 1;
        return new Vector2(x, y);
    }

    private Vector2 RemapCoordinates(Vector2 normalizedCoords, Vector2 min, Vector2 max)
    {
        float remappedX = Mathf.Lerp(min.x, max.x, (normalizedCoords.x + 1) / 2);
        float remappedY = Mathf.Lerp(min.y, max.y, (normalizedCoords.y + 1) / 2);
        return new Vector2(remappedX, remappedY);
    }

    private void OnGUI()
    {
        Utils.RealtimeDebug($"Screen Position: X{remappedCoords.x:F0}, Y{remappedCoords.y:F0}", new Vector2(10, 10), 24, Color.white, Color.black, 2, 4);
    }
}
