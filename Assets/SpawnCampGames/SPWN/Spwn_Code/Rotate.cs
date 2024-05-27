using UnityEngine;
using SPWN;

namespace SPWN
{
    public class Rotate : MonoBehaviour
    {
        [Header("Inputs")]
        public GameObject target;
        public Axis axis;
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
                switch(axis)
                {
                    case Axis.X: return targetObj.transform.right;
                    case Axis.Y: return targetObj.transform.up;
                    case Axis.Z: return targetObj.transform.forward;
                    default: return Vector3.zero;
                }
            }
            else
            {
                switch(axis)
                {
                    case Axis.X: return Vector3.right;
                    case Axis.Y: return Vector3.up;
                    case Axis.Z: return Vector3.forward;
                    default: return Vector3.zero;
                }
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
