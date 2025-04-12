using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public Transform startPoint; // Assign in Inspector
    public Transform endPoint;   // Assign in Inspector
    public Vector3 startOffset;  // Offset for the starting point

    public GameObject greenLine;
    public GameObject orangeLine;

    private LineRenderer mainLineRenderer;
    private LineRenderer normalizedLineRenderer;

    void Start()
    {

        // Instantiate two LineRenderers from the prefab
        mainLineRenderer = Instantiate(greenLine).GetComponent<LineRenderer>();
        normalizedLineRenderer = Instantiate(orangeLine).GetComponent<LineRenderer>();

        // Set initial settings (e.g., disabling both lines initially)
        mainLineRenderer.enabled = false;
        normalizedLineRenderer.enabled = false;

        // Optionally, parent the instantiated lines to the current object for better organization
        mainLineRenderer.transform.SetParent(transform);
        normalizedLineRenderer.transform.SetParent(transform);

        // Set the number of points for each LineRenderer
        mainLineRenderer.positionCount = 2;
        normalizedLineRenderer.positionCount = 2;

    }

    void Update()
    {
        // Check if the mouse button is pressed
        if (Input.GetMouseButtonDown(1))
        {
            // Enable both LineRenderers when the mouse button is pressed
            mainLineRenderer.enabled = true;
            normalizedLineRenderer.enabled = true;
        }

        // Check if the mouse button is released
        if (Input.GetMouseButtonUp(1))
        {
            // Disable both LineRenderers when the mouse button is released
            mainLineRenderer.enabled = false;
            normalizedLineRenderer.enabled = false;
        }

        // Update the positions of the lines if the mainLineRenderer is enabled
        if (mainLineRenderer.enabled)
        {
            // Update the main line
            Vector3 startPosition = startPoint.position + startOffset;
            Vector3 endPosition = endPoint.position;

            mainLineRenderer.SetPosition(0, startPosition);
            mainLineRenderer.SetPosition(1, endPosition);

            // Calculate and update the normalized direction line
            Vector3 direction = (endPosition - startPosition).normalized;
            normalizedLineRenderer.SetPosition(0, startPosition);
            normalizedLineRenderer.SetPosition(1, startPosition + direction); // Adjust length as needed
        }
    }
}
