using UnityEngine;
using SPWN;

namespace SPWN
{
    public class Rotate : MonoBehaviour
    {
        [Header("Inputs")]
        public GameObject target;
        public Direction axis;
        public bool local;

        GameObject targetObj;

        [Header("Rotation")]
        public float rotationSpeed = 10f;

        private void Awake()
        {
            targetObj = target != null ? target : this.gameObject;
        }

        void Update()
        {
            Vector3 rotationAxis = GetRotationAxis();
            RotateAroundAxis(rotationAxis);
        }

        Vector3 GetRotationAxis()
        {
            if(local)
            {
                return axis.RealDirection(targetObj.transform); // Using RealDirection for local axis
            }
            else
            {
                return axis.RealDirection(); // Using RealDirection for world axis
            }
        }

        void RotateAroundAxis(Vector3 rotationAxis)
        {
            // Create a quaternion representing the rotation around the axis
            Quaternion rotation = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime,rotationAxis);
            targetObj.transform.rotation = rotation * targetObj.transform.rotation;
        }

        private void OnDrawGizmos()
        {
            if(targetObj != null)
            {
                Vector3 rotationAxis = GetRotationAxis();
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(targetObj.transform.position,targetObj.transform.position + rotationAxis * 2);
            }
        }
    }
}
