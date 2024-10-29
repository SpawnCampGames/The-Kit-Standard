using UnityEngine;

public class CenterPointSpawner : MonoBehaviour
{
    public GameObject circlePrefab; // Assign the circle sprite prefab in the Inspector
    private GameObject spawnedCircle;

    void Start()
    {
        // Spawn the circle once at the start and keep reference to it
        Vector3 centerPoint = CalculateCenterPoint();
        spawnedCircle = Instantiate(circlePrefab, centerPoint, Quaternion.identity);
    }

    void Update()
    {
        // Continuously update the position of the spawned circle to the center of the children
        Vector3 centerPoint = CalculateCenterPoint();
        KeepCircleCentered(centerPoint);
    }

    Vector3 CalculateCenterPoint()
    {
        Vector3 totalPosition = Vector3.zero;
        int childCount = transform.childCount;

        if (childCount == 0) return transform.position; // No children, return the object's own position

        // Loop through all child objects and sum their positions
        for (int i = 0; i < childCount; i++)
        {
            totalPosition += transform.GetChild(i).position;
        }

        // Get the average position (center point)
        return totalPosition / childCount;
    }

    void KeepCircleCentered(Vector3 center)
    {
        if (spawnedCircle != null)
        {
            spawnedCircle.transform.position = center;
        }
    }
}
