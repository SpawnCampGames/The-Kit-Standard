using UnityEngine;
namespace SPWN
{
    public class CollisionByTag : MonoBehaviour
    {
        public string TagToDetect;

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("A character controller or gameobject w/ a rigidbody has collided."); // Log when anything collides

            if(collision.gameObject.CompareTag(TagToDetect))
            {
                Debug.Log($"{collision.gameObject.name} is tagged with: {TagToDetect} and has been detected colliding"); // Log when tagged with TagToDetect
            }
        }
    }
}

