// RX.cs
using UnityEngine;

public class RX : MonoBehaviour
{
    public int frequency = 1; // Default frequency
    public float maxReceptionDistance = 15f; // Reception range of the receiver


    void Update()
    {
        Debug.Log($"Signal Strength is {GetSignalStrength()}");
    }
    public float GetSignalStrength()
    {
        float signalStrength = 0f;

        // Iterate over all TX objects in the scene
        TX[] txObjects = FindObjectsByType<TX>(FindObjectsSortMode.None);
        foreach (TX tx in txObjects)
        {
            // Calculate signal strength using TX's max transmission distance and frequency
            signalStrength += CalculateSignalStrength(tx.frequency, tx.maxTransmissionDistance, tx.transform.position);
        }

        return signalStrength;
    }

    float CalculateSignalStrength(int txFrequency, float txMaxTransmissionDistance, Vector3 txPosition)
    {
        float combinedRange = maxReceptionDistance + txMaxTransmissionDistance;
        float distance = Vector3.Distance(transform.position, txPosition); // Assuming RX's position

        // If within combined range and frequencies match, calculate signal strength
        if (distance <= combinedRange && frequency == txFrequency)
        {
            return Mathf.Clamp01(1 - distance / combinedRange);
        }
        else
        {
            // Out of combined range or frequencies don't match, signal strength is 0
            return 0f;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, maxReceptionDistance);
    }
}