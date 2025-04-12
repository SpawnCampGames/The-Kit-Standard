using UnityEngine;
using SPWN;

public class FuturePosition : MonoBehaviour
{
    public Transform marker;
    public float speedOfIncomingBullet = 50f; // The speed of the projectile fired by the turret
    public Vector3 initialVelocity = new Vector3(0, 0, 100); // Initial velocity of the rocket, adjustable in Inspector

    private Rigidbody rb;
    [SerializeField] private Transform aa_location;

    void Start()
    {
        // Disconnect the special marker from the Rigidbody
        marker.transform.parent = null;

        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Dbug.Error("Rigidbody component not found on this GameObject.");
            return;
        }

        if (aa_location == null)
        {
            Dbug.Error("Turret GameObject not found in the scene.");
            return;
        }
        transform.TransformDirection(initialVelocity);
        
        // Set the special marker at the initial position
        if (marker != null)
        {
            marker.transform.position = transform.position;
        }
        else
        {
            Debug.LogWarning("Special marker not assigned.");
        }
    }

    void Update()
    {
        if (rb != null && aa_location != null)
        {
            // Calculate the distance from this object to the turret
            float distanceToTurret = Vector3.Distance(transform.position, aa_location.position);

            // Calculate the time to predict based on the distance and speed of the incoming bullet
            float timeToPredict = distanceToTurret / speedOfIncomingBullet;

            // Calculate future position based on current velocity and time ahead
            Vector3 futurePosition = transform.position + rb.linearVelocity * timeToPredict;

            // Check if gravity is enabled on the Rigidbody
            if (rb.useGravity)
            {
                // Calculate the effect of gravity over the time ahead
                Vector3 gravityEffect = 0.5f * Physics.gravity * Mathf.Pow(timeToPredict, 2);
                futurePosition += gravityEffect;
            }

            // Set the special marker at the final predicted position
            if (marker != null)
            {
                marker.transform.position = futurePosition;
            }
        }
    }

    public Transform GetSpecialMarker()
    {
        return marker.transform;
    }
}
