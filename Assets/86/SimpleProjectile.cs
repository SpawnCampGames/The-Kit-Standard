using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    public float force = 10f;

    public void DebugName()
    {
        Debug.Log($"{gameObject.name} has joined the party");
    }
}
