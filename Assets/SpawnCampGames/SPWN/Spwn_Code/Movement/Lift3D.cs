using UnityEngine;

public class Lift3D : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask pickupLayer;
    public float springForce = 500f;
    public float damping = 50f;
    public float liftForce = 10f;
    public float presetDistance = 5f; // Distance from the camera where the object should be kept
    public float pickupRange = 10f; // Maximum range within which objects can be picked up
    public float releaseDampingFactor = 0.5f; // Factor to reduce the velocity when releasing the object

    private Rigidbody pickedObject;
    private Vector3 targetPosition;
    private Vector3 pickupOffset;
    private bool isPickingUp;

    void Update()
    {
        // Raycast to get the point in front of the camera
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Vector3 followPosition = ray.GetPoint(presetDistance);

        if (Input.GetMouseButtonDown(0))
        {
            TryPickup(ray);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Release();
        }

        if (isPickingUp)
        {
            // Update targetPosition to be at the preset distance from the camera
            targetPosition = followPosition;
        }
    }

    void FixedUpdate()
    {
        if (isPickingUp && pickedObject != null)
        {
            Vector3 currentPosition = pickedObject.position;
            Vector3 direction = targetPosition - currentPosition;
            float distance = direction.magnitude;

            // Calculate force vectors
            Vector3 springForceVector = direction.normalized * springForce * distance;
            Vector3 dampingForce = pickedObject.linearVelocity * damping;
            Vector3 liftForceVector = Vector3.up * liftForce;

            Vector3 force = springForceVector - dampingForce + liftForceVector;

            // apply forces at the pickup offset position
            //pickedObject.AddForceAtPosition(force, pickedObject.position + pickupOffset, ForceMode.Acceleration);

            pickedObject.AddForce(force, ForceMode.Acceleration);
        }
    }

    void TryPickup(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
        {
            float distanceToHit = Vector3.Distance(mainCamera.transform.position, hit.point);

            if (distanceToHit <= pickupRange)
            {
                pickedObject = hit.collider.GetComponent<Rigidbody>();
                if (pickedObject != null)
                {
                    // Set the target position to be at the preset distance from the camera
                    targetPosition = ray.GetPoint(presetDistance);

                    // Calculate the offset from the object's center to the hit point
                    pickupOffset = hit.point - pickedObject.position;

                    isPickingUp = true;
                    pickedObject.useGravity = false;
                }
            }
        }
    }

    void Release()
    {
        if (pickedObject != null)
        {
            // Apply velocity damping to reduce the object's velocity when released
            pickedObject.linearVelocity *= releaseDampingFactor;

            isPickingUp = false;
            pickedObject.useGravity = true;
            pickedObject = null;
        }
    }

    void OnDrawGizmos()
    {
        if (pickedObject != null && isPickingUp)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pickedObject.position, targetPosition);

            Vector3 direction = targetPosition - pickedObject.position;
            float distance = direction.magnitude;

            // Draw spring force
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pickedObject.position, pickedObject.position + direction.normalized * springForce * distance / 1000f);

            // Draw damping force
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(pickedObject.position, pickedObject.position - pickedObject.linearVelocity.normalized * damping / 100f);

            // Draw lift force
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pickedObject.position, pickedObject.position + Vector3.up * liftForce / 10f);
        }
    }
}
