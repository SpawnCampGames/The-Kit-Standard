using UnityEngine;
using System.Collections.Generic;

public class SpawnDrawLineRenderer : MonoBehaviour
{
    public GameObject linePrefab; // Prefab with LineRenderer attached
    public Transform lineParent; // Parent object to organize lines
    public float lineWidth = 0.1f;

    private LineRenderer currentLineRenderer;
    private List<Vector3> points = new List<Vector3>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateNewLine(); // Start a new line
        }

        if (Input.GetMouseButton(0) && currentLineRenderer != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure the line is drawn on the 2D plane

            if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], mousePosition) > 0.1f)
            {
                points.Add(mousePosition);
                currentLineRenderer.positionCount = points.Count;
                currentLineRenderer.SetPositions(points.ToArray());
            }
        }
    }

    void CreateNewLine()
    {
        // Instantiate a new line from the prefab
        GameObject newLine = Instantiate(linePrefab, lineParent);
        currentLineRenderer = newLine.GetComponent<LineRenderer>();

        // Set the line properties
        currentLineRenderer.startWidth = lineWidth;
        currentLineRenderer.endWidth = lineWidth;
        currentLineRenderer.positionCount = 0;

        // Clear the points for the new line
        points.Clear();
    }
}
