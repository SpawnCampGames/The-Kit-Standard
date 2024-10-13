using UnityEngine;

public class MovementDebugger : MonoBehaviour
{
    public enum UpdateMode
    {
        All,
        Update,
        FixedUpdate,
        LateUpdate,
    }

    public UpdateMode updateMode = UpdateMode.All;

    public float logDuration = 10f;
    public float lineHeight = 0.25f;

    public Color updateColor = Color.magenta;
    public Color fixedUpdateColor = Color.blue;
    public Color lateUpdateColor = Color.green;

    private float updateOffset = 0.5f;
    private float fixedOffset = 0.25f;
    private float lateOffset = 0.1f;

    void Update()
    {
        if (updateMode == UpdateMode.Update || updateMode == UpdateMode.All)
            DrawLine(updateColor, updateOffset);
    }

    void FixedUpdate()
    {
        if (updateMode == UpdateMode.FixedUpdate || updateMode == UpdateMode.All)
            DrawLine(fixedUpdateColor, fixedOffset);
    }

    void LateUpdate()
    {
        if (updateMode == UpdateMode.LateUpdate || updateMode == UpdateMode.All)
            DrawLine(lateUpdateColor, lateOffset);
    }

    void DrawLine(Color color, float verticalOffset)
    {
        // Calculate the start and end positions for the line with the vertical offset
        Vector3 startPosition = transform.position + Vector3.up * verticalOffset;
        Vector3 endPosition = startPosition + Vector3.up * lineHeight;

        // Draw the line with the specified color and duration
        Debug.DrawLine(startPosition, endPosition, color, logDuration);
    }
}
