using UnityEngine;

public class Lift3D : MonoBehaviour
{
    Camera mainCamera; // assign via scripting for cinemachine and non-cinemachine setups
    public LayerMask pickupLayer;
    public float springForce = 500f;
    public float damping = 50f;
    public float liftForce = 10f;
    public float presetDistance = 2f;
    public float pickupRange = 2.5f;
    public float releaseDampingFactor = 0.5f;

    private Rigidbody pickedObject;
    private Vector3 targetPosition;
    private bool isPickingUp;

    private void Start()
    {
        mainCamera = Camera.main;

        //error check
        if(mainCamera == null)
        {
            Debug.Log("Camera could not be found.\nMake sure you have a Camera in your scene and its assigned to the MainCamera Tag");
        }

    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2,Screen.height / 2,0));
        Vector3 followPosition = ray.GetPoint(presetDistance);

        if(Input.GetMouseButtonDown(0)) TryPickup(ray);
        if(Input.GetMouseButtonUp(0)) Release();

        if(isPickingUp) targetPosition = followPosition;
    }

    void FixedUpdate()
    {
        if(!isPickingUp || pickedObject == null) return;

        Vector3 direction = targetPosition - pickedObject.position;
        float distance = direction.magnitude;

        Vector3 force = direction.normalized * springForce * distance - pickedObject.linearVelocity * damping + Vector3.up * liftForce;
        pickedObject.AddForce(force,ForceMode.Acceleration);
    }

    void TryPickup(Ray ray)
    {
        if(!Physics.Raycast(ray,out RaycastHit hit,pickupRange * 0.5f,pickupLayer)) return;

        if(Vector3.Distance(mainCamera.transform.position,hit.point) > pickupRange) return;

        pickedObject = hit.collider.GetComponent<Rigidbody>();
        if(pickedObject == null) return;

        targetPosition = ray.GetPoint(presetDistance);
        isPickingUp = true;
        pickedObject.useGravity = false;
    }

    void Release()
    {
        if(pickedObject == null) return;

        pickedObject.linearVelocity *= releaseDampingFactor;
        pickedObject.useGravity = true;
        isPickingUp = false;
        pickedObject = null;
    }

    void OnDrawGizmos()
    {
        if(pickedObject == null || !isPickingUp) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(pickedObject.position,targetPosition);

        Vector3 direction = targetPosition - pickedObject.position;
        float distance = direction.magnitude;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pickedObject.position,pickedObject.position + direction.normalized * springForce * distance / 1000f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pickedObject.position,pickedObject.position - pickedObject.linearVelocity.normalized * damping / 100f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(pickedObject.position,pickedObject.position + Vector3.up * liftForce / 10f);
    }
}
