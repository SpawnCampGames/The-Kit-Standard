using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    // Target to follow
    public Transform core;

    // Speed modifier
    public float followSpeed = 5f;

    void Start()
    {
        // Ensure target is not parented
        core.parent = null;
    }

    void Update()
    {
        if (core == null) return;

        // Smoothly follow the target using Lerp
        transform.position = Vector3.Lerp(transform.position, core.position, followSpeed * Time.deltaTime);
    }
}
