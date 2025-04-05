using UnityEngine;

/// <summary>
/// <para><c>RotateMark</c> script to rotate a specified Transform.</para>
/// <para>Allows for flexible rotation with configurable settings:</para>
/// <list type="bullet">
/// <item>Set a custom <c>Transform</c> to rotate.</item>
/// <item>Define rotation speed and direction.</item>
/// <item>Automatically assigns this GameObject's <c>Transform</c> if none is provided.</item>
/// </list>
/// <remarks>
/// Included in: The L.A.B.
/// </remarks>
/// </summary>
public class RotateMark : MonoBehaviour
{
    public Transform mark;
    public Vector3 rotationDirection = Vector3.up;
    public float rotationSpeed;

    private void Start()
    {
        if(!mark && TryGetComponent(out Transform foundTransform))
            mark = foundTransform;
    }

    private void Update()
    {
        if(!mark) return;
        mark.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
    }
}
