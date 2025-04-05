using UnityEngine;
using System.Collections.Generic;

public class TriggerDirectionDebug : MonoBehaviour
{
    private struct HitData
    {
        public Vector3 origin;
        public Vector3 direction;
    }

    private List<HitData> hitDirections = new();

    public float rayLength = 2f;  // Ray length
    public float sphereRadius = 0.5f;  // Sphere collider radius

    private void OnTriggerEnter(Collider other)
    {
        // Calculate the direction of impact (from other object to this object)
        Vector3 impactDirection = (transform.position - other.transform.position).normalized;

        // Rebound direction is the opposite of the impact direction
        Vector3 reboundDirection = -impactDirection;

        // Move the origin out to the edge of the sphere (on the opposite side of the impact)
        Vector3 offsetOrigin = other.transform.position - impactDirection * sphereRadius;

        // Add the rebound ray to the list
        hitDirections.Add(new HitData
        {
            origin = offsetOrigin,
            direction = reboundDirection * rayLength  // Extend the rebound ray
        });

        Debug.Log($"Hit from {other.name} | Rebound Direction: {reboundDirection} | Ray from {offsetOrigin}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        foreach(var hit in hitDirections)
            Gizmos.DrawRay(hit.origin,hit.direction);  // Draw the rebound ray
    }
}
