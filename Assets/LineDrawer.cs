using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public Transform startPoint; // Assign in Inspector
    public Transform endPoint; // Assign in Inspector
    public Vector3 startOffset; // Offset for the starting point

    private LineRenderer lineRenderer;

    void Start()
    {
        // Get the LineRenderer component attached to this GameObject
        lineRenderer = GetComponent<LineRenderer>();

        // Set the number of points to 2
        lineRenderer.positionCount = 2;

        // Initially disable the LineRenderer
        lineRenderer.enabled = false;
    }

    void Update()
    {
        // Check if the mouse button is pressed
        if (Input.GetMouseButtonDown(1))
        {
            // Enable the LineRenderer when the mouse button is pressed
            lineRenderer.enabled = true;
        }

        // Check if the mouse button is released
        if (Input.GetMouseButtonUp(1))
        {
            // Disable the LineRenderer when the mouse button is released
            lineRenderer.enabled = false;
        }

        // Update the positions of the line's start and end points
        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, startPoint.position + startOffset); // Apply the offset to the start point
            lineRenderer.SetPosition(1, endPoint.position);
        }
    }
}
