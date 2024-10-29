using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    // Target to follow
    public Transform core;

    // Speed modifier
    public float followSpeed = 5f;

    // Smoothing parameters
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero; // Used by SmoothDamp to keep track of current velocity

    void Update()
    {
        if (core == null) return;

        // Smoothly move the position towards the target
        transform.position = Vector3.SmoothDamp(
            transform.position, // Current position
            core.position,      // Target position
            ref velocity,       // Reference to the current velocity (modified by SmoothDamp)
            smoothTime,         // Time to smooth the movement
            followSpeed,        // Maximum speed (optional, affects how quickly it can move towards the target)
            Time.deltaTime);   // Delta time for frame-rate independence
    }
}
