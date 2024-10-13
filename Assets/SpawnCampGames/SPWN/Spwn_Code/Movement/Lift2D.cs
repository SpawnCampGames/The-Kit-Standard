using UnityEngine;

public class SpringPickup2D : MonoBehaviour
{
    public Transform crosshairTransform; // Reference to the Crosshair GameObject
    public LayerMask pickupLayer; // Layer for objects that can be picked up
    public float springForce = 10f; // Force applied by the spring
    public float damping = 0.5f; // Damping to reduce spring oscillation
    public float overshootAmount = 1f; // Amount to overshoot
    public float liftForce = 5f; // Extra force to counteract gravity

    private Rigidbody2D pickedObject; // The currently picked up object
    private Vector2 targetPosition; // The target position to move towards
    private bool isPickingUp; // Flag to check if an object is picked up

    void Update()
    {
        Vector3 followPosition = crosshairTransform.position;

        if (Input.GetMouseButtonDown(0))
        {
            TryPickup(followPosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Release();
        }

        if (isPickingUp)
        {
            // Calculate the direction and apply force to counteract gravity
            if (pickedObject != null)
            {
                Vector2 currentPosition = pickedObject.position;
                Vector2 direction = (targetPosition - currentPosition).normalized;
                float distance = Vector2.Distance(currentPosition, targetPosition);

                // Apply spring force
                pickedObject.position = Vector2.Lerp(currentPosition, targetPosition, Time.deltaTime * springForce);

                // Apply lift force to counteract gravity
                Vector2 lift = direction * liftForce * Time.deltaTime;
                pickedObject.linearVelocity = new Vector2(pickedObject.linearVelocity.x, lift.y);

                // Update the target position
                targetPosition = followPosition;
            }
        }
    }

    void TryPickup(Vector3 followPosition)
    {
        Collider2D hit = Physics2D.OverlapPoint(followPosition, pickupLayer);
        if (hit != null)
        {
            pickedObject = hit.GetComponent<Rigidbody2D>();
            if (pickedObject != null)
            {
                targetPosition = followPosition;
                isPickingUp = true;
                // Ensure the Rigidbody2D is not affected by other forces
                pickedObject.gravityScale = 0;
            }
        }
    }

    void Release()
    {
        if (pickedObject != null)
        {
            isPickingUp = false;
            // Reset gravity scale
            pickedObject.gravityScale = 1;
        }
    }
}
