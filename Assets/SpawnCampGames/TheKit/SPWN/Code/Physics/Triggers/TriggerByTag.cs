using UnityEngine;
namespace SPWN
{
    public class TriggerByTag : MonoBehaviour
    {
        public string TagToDetect;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("A character controller or gameobject w/ a rigidbody has entered the trigger"); // Log when anything enters the trigger

            if(other.CompareTag(TagToDetect))
            {
                Debug.Log($"{other.name} is tagged with: {TagToDetect} and has been detected entering the trigger"); // Log when matching Tag enters the trigger
            }
        }
    }
}
