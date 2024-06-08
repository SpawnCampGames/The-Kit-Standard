using UnityEngine;

public class Tank : MonoBehaviour
{
    private Rigidbody rb;
    public Transform leftTrack;
    public Transform rightTrack;
    public float speed = 10f;
    public float turnRate = 5f;

    public Vector3 forwardForce;
    public float turnForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        forwardForce = transform.forward * move * speed;

        turnForce = turn * turnRate * speed;

        Vector3 leftTrackForce = forwardForce + (transform.forward * turnForce / 2f);
        Vector3 rightTrackForce = forwardForce + (-transform.forward * turnForce / 2f);

        rb.AddForceAtPosition(leftTrackForce, leftTrack.position);
        rb.AddForceAtPosition(rightTrackForce, rightTrack.position);
    }
}
