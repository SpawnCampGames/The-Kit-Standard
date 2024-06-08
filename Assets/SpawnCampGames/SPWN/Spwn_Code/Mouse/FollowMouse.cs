using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public bool is2D;
    public float distanceFromCamera = 10f; // Distance from the camera to place the object    
    public bool useRaycast;
    public float raycastOffset;
    public bool useNormal;
    public bool hideCursor;
    public bool lockCursor;

    private void Awake()
    {
        Cursor.visible = !hideCursor;
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
    }

    void Update()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;

        /// <summary>
        /// Use Raycast Method for Casting into 3D Environments
        /// </summary>
        if (useRaycast)
        {
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 hitPosition = hit.point;
                transform.position = new Vector3(hitPosition.x, hitPosition.y, hitPosition.z + raycastOffset);
                
                if (useNormal)
                {
                    transform.up = hit.normal;
                }
            }
        }
        else if (is2D)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, distanceFromCamera));
            transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
        }
        else
        {
            float distanceToPlane = distanceFromCamera;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, distanceToPlane));
            transform.position = mouseWorldPosition;
        }
    }
}
