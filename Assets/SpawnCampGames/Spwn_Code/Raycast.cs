using UnityEditor;
using UnityEngine;

namespace SPWN
{
    public class Raycast : MonoBehaviour
    {
        [Header("Inputs")]
        public GameObject target;
        public Direction direction;
        public Vector3 customDirection;
        public bool local;

        Vector3 targetDir;
        GameObject targetObj;

        [Header("Graphics")]
        public Color rayColor = Color.green;
        public Color rayHitColor = Color.red;
        public float rayDistance = 10f;
        public LayerMask layers;
        public bool draw = true;

        [Header("Outputs")]
        bool success;

        public GameObject hit;
        RaycastHit tempHit;

        private void Awake()
        {
            if(target != null)
                targetObj = target;
            else
                targetObj = this.gameObject;
        }

        void CalculateDirections()
        {
            if(customDirection != Vector3.zero)
            {
                targetDir = customDirection;
            }
            else
            {
                targetDir = local ? (
                            direction == Direction.North ? targetObj.transform.forward :
                            direction == Direction.West ? -targetObj.transform.right :
                            direction == Direction.South ? -targetObj.transform.forward :
                            direction == Direction.East ? targetObj.transform.right :
                            direction == Direction.Up ? targetObj.transform.up :
                            -targetObj.transform.up
                        ) : (
                            direction == Direction.North ? Vector3.forward :
                            direction == Direction.West ? -Vector3.right :
                            direction == Direction.South ? -Vector3.forward :
                            direction == Direction.East ? Vector3.right :
                            direction == Direction.Up ? Vector3.up :
                            -Vector3.up
                        );
            }
        }

        void Update()
        {
            CalculateDirections();

            if (layers != 0) // Check if layers is set to a specific layer mask
            {
                // Raycast with the specified layer mask
                if(Physics.Raycast(transform.position,targetDir,out tempHit,rayDistance,layers))
                {
                    hit = tempHit.collider.gameObject;
                    success = true;
                }
                else
                {
                    hit = null;
                    success = false;
                }
            }
            else
            {
                // Raycast without specifying a layer mask, hitting all layers
                if(Physics.Raycast(transform.position,targetDir,out tempHit,rayDistance))
                {
                    hit = tempHit.collider.gameObject;
                    success = true;
                }
                else
                {
                    hit = null;
                    success = false;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if(draw)
            {
                if(success)
                {
                    Gizmos.color = rayHitColor;
                }
                else
                {
                    Gizmos.color = rayColor;
                }

                Gizmos.DrawRay(transform.position, targetDir * rayDistance);

                // Draw circle gizmo flat on the y-plane of the hit GameObject's normal
                if (hit != null)
                {
                    Vector3 circleCenter = tempHit.point;
                    Vector3 hitNormal = tempHit.normal;
                    Vector3 circleNormal = hitNormal == Vector3.zero ? Vector3.up : hitNormal; // Ensure the normal vector is not zero
                    Vector3 circleUp = Vector3.Cross(Vector3.up,circleNormal).normalized; // Calculate an up vector orthogonal to the normal
                    Handles.color = Gizmos.color;
                    Handles.DrawWireDisc(circleCenter,circleNormal,.1f); // Draw a circle with a radius of 1
                }
            }
        }
    }
}