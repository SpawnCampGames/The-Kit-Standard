using SPWN;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [Header("DEBUG PURPOSES")]
    public GameObject extraArrows;
    Transform extraArrowsTransform;

    public enum FollowMode { Mode2D, Mode3D, ModeUI }
    public enum Plane2D { XY, XZ }

    [Tooltip("Select the follow mode for the object.")]
    public FollowMode currentFollowMode = FollowMode.Mode3D;

    [Header("GENERAL")]
    public float cameraDistance = 10f;

    [Header("2D Mode Settings")]
    public Plane2D plane2D = Plane2D.XY;

    [Header("3D Mode Settings")]
    public float raycastOffset = 0.5f;
    public bool use3DNormal = true;

    [Header("Cursor Settings")]
    public bool hideCursor = true;
    public bool lockCursor = false;

    [Header("SCREEN REMAP")]
    public Vector2 minCoords = new Vector2(-10,-10);
    public Vector2 maxCoords = new Vector2(10,10);

    [Header("WORLD ACTUAL")]
    public Vector3 worldMin = new Vector3(-250f,0f,-250f);
    public Vector3 worldMax = new Vector3(250f,0f,250f);

    [Header("WORLD REMAP")]
    public Vector2 worldMinRemap = new Vector2(-1,-1);  // Remapped Min Range
    public Vector2 worldMaxRemap = new Vector2(1,1);    // Remapped Max Range

    [Header("RESULTS")]
    public Vector2 remappedWorldCoords;
    public Vector2 remappedMouseCoords;
    public Vector2 mouseCoords;
    public Vector3 worldCoords;

    private RectTransform uiElement;
    bool isStandby = false;

    private void Awake()
    {
        // spawn extra arrows
        extraArrowsTransform = Instantiate(extraArrows,Vector3.zero,Quaternion.identity).transform;
        InitializeUIElement();
    }

    private void InitializeUIElement()
    {
        if(currentFollowMode == FollowMode.ModeUI)
        {
            uiElement = GetComponent<RectTransform>();
            if(uiElement != null) return;

            Debug.LogWarning("UI Follow Mode requires a RectTransform component.");
        }
    }

    void Update()
    {
        if(isStandby) return;

        if(Cursor.lockState != CursorLockMode.Locked)
        {
            FollowMousePosition();
        }
        else
        {
            ResetPosition();
        }
    }

    void FollowMousePosition()
    {
        mouseCoords = Input.mousePosition;

        // Calculate raw and remapped screen coordinates
        Vector2 normalizedScreenCoords = GetNormalizedCoordinates(mouseCoords);
        remappedMouseCoords = RemapCoordinates(normalizedScreenCoords,minCoords,maxCoords);

        // Calculate raw and remapped world coordinates
        worldCoords = CalculateWorldCoordinates(mouseCoords);
        remappedWorldCoords = RemapCoordinatesWorld(worldCoords,worldMin,worldMax,worldMinRemap,worldMaxRemap);
        //UIManager.Instance.UpdateWorldMouse(worldCoords,remappedWorldCoords);

        extraArrowsTransform.position = worldCoords;

        // Call mode-specific follow functions
        switch(currentFollowMode)
        {
            case FollowMode.Mode3D:
            FollowIn3D(worldCoords);
            break;
            case FollowMode.Mode2D:
            FollowIn2D(mouseCoords);
            break;
            case FollowMode.ModeUI:
            FollowInUI(mouseCoords);
            break;
        }
    }

    private Vector3 CalculateWorldCoordinates(Vector3 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        return Physics.Raycast(ray,out RaycastHit hit) ? hit.point : Vector3.zero;
    }

    private void FollowIn3D(Vector3 worldPosition)
    {
        if(worldPosition != Vector3.zero)
        {
            transform.position = worldPosition + Vector3.up * raycastOffset;

            if(use3DNormal)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray,out RaycastHit hit))
                {
                    transform.up = hit.normal;
                }
            }
        }
    }

    private void FollowIn2D(Vector3 mouseScreenPos)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x,mouseScreenPos.y,cameraDistance));

        if(plane2D == Plane2D.XY)
            transform.position = new Vector3(mouseWorldPos.x,mouseWorldPos.y,transform.position.z);
        else
            transform.position = new Vector3(mouseWorldPos.x,transform.position.y,mouseWorldPos.z);
    }

    private void FollowInUI(Vector3 mouseScreenPos)
    {
        if(uiElement == null) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiElement.parent as RectTransform,mouseScreenPos,null,out Vector2 localPoint
        );
        uiElement.localPosition = localPoint;
    }

    private void ResetPosition()
    {
        switch(currentFollowMode)
        {
            case FollowMode.Mode3D:
            transform.position = Vector3.zero;
            break;
            case FollowMode.Mode2D:
            transform.position = new Vector3(0,0,cameraDistance);
            break;
            case FollowMode.ModeUI:
            if(uiElement != null) uiElement.localPosition = Vector2.zero;
            break;
        }
    }

    private Vector2 GetNormalizedCoordinates(Vector3 screenPosition)
    {
        float x = (screenPosition.x / Screen.width) * 2 - 1;
        float y = (screenPosition.y / Screen.height) * 2 - 1;
        return new Vector2(x,y);
    }

    private Vector2 RemapCoordinates(Vector2 normalizedCoords,Vector2 min,Vector2 max)
    {
        float remappedX = Mathf.Lerp(min.x,max.x,(normalizedCoords.x + 1) / 2);
        float remappedY = Mathf.Lerp(min.y,max.y,(normalizedCoords.y + 1) / 2);
        return new Vector2(remappedX,remappedY);
    }

    // Updated RemapCoordinatesWorld function
    private Vector3 RemapCoordinatesWorld(Vector3 worldCoords,Vector3 min,Vector3 max,Vector2 worldMinRemap,Vector2 worldMaxRemap)
    {
        // Normalize world coordinates based on actual world space range (worldMin -> worldMax)
        float normalizedX = Mathf.InverseLerp(min.x,max.x,worldCoords.x);
        float normalizedZ = Mathf.InverseLerp(min.z,max.z,worldCoords.z);

        // Remap the normalized world coordinates to the remapped range (worldMinRemap -> worldMaxRemap)
        float remappedX = Mathf.Lerp(worldMinRemap.x,worldMaxRemap.x,normalizedX);
        float remappedZ = Mathf.Lerp(worldMinRemap.y,worldMaxRemap.y,normalizedZ);

        return new Vector2(remappedX,remappedZ);
    }

    private void OnDrawGizmos()
    {
        if(Mouse.TryGetWorldRaycast(Camera.main,out RaycastHit hit))
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(hit.point,0.1f);
            Gizmos.DrawRay(hit.point,hit.normal * 10);
        }
    }

    private void OnGUI()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        // Format the output with line breaks for better readability
        string displayText = $"SCREEN:\n" +
                             $"FACTORY: ({mouseScreenPos.x:0.00}, {mouseScreenPos.y:0.00})\n" +
                             $"RMAP: ({remappedMouseCoords.x:0.00}, {remappedMouseCoords.y:0.00})";

        // Call the static method from the Utils class to display the formatted message
        Utils.RealtimeDebug(displayText,new Vector2(20,10),24,Color.white,Color.black,2,4);

        string worldDisplayText = $"WORLD:\n" +
                                  $"POSITION: ({worldCoords.x:0.00}, {worldCoords.y:0.00}, {worldCoords.z:0.00})\n" +
                                  $"REMAP: ({remappedWorldCoords.x:0.00}, {remappedWorldCoords.y:0.00})";

        Utils.RealtimeDebug(worldDisplayText,new Vector2(20,140),24,Color.green,Color.black,2,4);
    }
}
