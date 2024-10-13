using UnityEngine;

public class BulletImpact : MonoBehaviour
{

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        SPWN.Dbug.Green($"Bullet impact on {other.gameObject.name}");
        rb.useGravity = true;
        Destroy(this.gameObject,1f);
    }
}
