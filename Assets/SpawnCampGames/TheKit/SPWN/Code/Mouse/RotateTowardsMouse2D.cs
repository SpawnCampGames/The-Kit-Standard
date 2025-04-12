using UnityEngine;

public class RotateTowardsMouse2D : MonoBehaviour
{
    //cheeky lil change
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        Vector3 direction = mousePosition - transform.position;

        transform.up = direction.normalized;
    }
}
