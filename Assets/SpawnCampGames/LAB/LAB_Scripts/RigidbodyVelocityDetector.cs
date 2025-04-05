using UnityEngine;

/// <summary>
/// <para><c>RigidbodyVelocityDetector</c> script that detects the velocity of a Rigidbody when it enters the trigger area.</para>
/// <para>Performs the following actions:</para>
/// <list type="bullet">
/// <item>Scales down the Rigidbody's velocity using a configurable scale factor.</item>
/// <item>Clamps the scaled velocity between 0 and 9999.</item>
/// <item>Sends the scaled velocity value to the <c>RangeClock</c> for display.</item>
/// </list>
/// </summary>
public class RigidbodyVelocityDetector : MonoBehaviour
{
    public RangeClock rangeClock;
    public float velocityScaleFactor = 10f;  // Results divided by ScaleFactor

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent(out Rigidbody rb)) return; // If not a rigidbody skip rest

        float velocityValue = rb.linearVelocity.magnitude / velocityScaleFactor;
        int scaledVelocity = Mathf.FloorToInt(velocityValue);
        scaledVelocity = Mathf.Clamp(scaledVelocity,0,9999);

        rangeClock.SetNumber(scaledVelocity,1.5f);
    }
}