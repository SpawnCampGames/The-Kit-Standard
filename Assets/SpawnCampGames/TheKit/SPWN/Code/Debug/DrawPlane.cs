using UnityEngine;

public class DrawPlane : MonoBehaviour
{
    public float width = 4f; 
    public float height = 4f; 
    public Color gizmoColor = Color.green;

    void OnDrawGizmos()
    {
        Vector3 center = transform.position;

        Vector3 topLeft = center + new Vector3(-width / 2,0,height / 2);
        Vector3 topRight = center + new Vector3(width / 2,0,height / 2);
        Vector3 bottomLeft = center + new Vector3(-width / 2,0,-height / 2);
        Vector3 bottomRight = center + new Vector3(width / 2,0,-height / 2);

        Gizmos.color = gizmoColor;

        Gizmos.DrawLine(topLeft,topRight);
        Gizmos.DrawLine(topRight,bottomRight);
        Gizmos.DrawLine(bottomRight,bottomLeft);
        Gizmos.DrawLine(bottomLeft,topLeft);
    }
}
