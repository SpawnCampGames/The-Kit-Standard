using UnityEngine;

public static class RigidbodyExtensions
{
    public static void SetVelocityX(this Rigidbody rigidbody, float x)
    {
        rigidbody.linearVelocity = new Vector3(x, rigidbody.linearVelocity.y, rigidbody.linearVelocity.z);
    }

    public static void SetVelocityY(this Rigidbody rigidbody, float y)
    {
        rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, y, rigidbody.linearVelocity.z);
    }

    public static void SetVelocityZ(this Rigidbody rigidbody, float z)
    {
        rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, rigidbody.linearVelocity.y, z);
    }
}