// TX.cs
using UnityEngine;

public class TX : MonoBehaviour
{
    public int frequency = 1; // Frequency of the transmitter
    public float maxTransmissionDistance = 10f; // Transmission range of the transmitter

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, maxTransmissionDistance);
    }
}
