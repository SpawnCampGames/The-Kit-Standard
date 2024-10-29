using UnityEngine;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the trigger"); // Log when anything enters the trigger

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger"); // Log when the player enters the trigger
        }
    }
}
