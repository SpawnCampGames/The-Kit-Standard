using UnityEngine;

public class TriggerEnter2D_Example : MonoBehaviour
{
    public string ComparisonTag = "Player";

    private void OnTriggerEnter2D(Collider2D other) {

        Debug.Log($"Something collided with our trigger.. {other.name}");

        if(other.CompareTag(ComparisonTag))
        {
            Debug.Log($"We collided with a gameobject tagged as {ComparisonTag}");
        }
    }
}
