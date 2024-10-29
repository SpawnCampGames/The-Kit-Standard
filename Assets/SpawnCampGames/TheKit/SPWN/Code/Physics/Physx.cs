using UnityEngine;
namespace SPWN
{
    public static class Physx
    {
        public static bool OverlapCylinder(Vector3 checkPosition, Vector3 cylinderCenter, float radius, float absoluteHeight = 0f)
        {
            Vector3 pointXZ = new Vector3(checkPosition.x, 0, checkPosition.z);
            Vector3 centerXZ = new Vector3(cylinderCenter.x, 0, cylinderCenter.z);
            float distance = Vector3.Distance(pointXZ, centerXZ);

            bool withinRadius = distance <= radius;
            bool withinHeight = absoluteHeight == 0 || Mathf.Abs(checkPosition.y - cylinderCenter.y) <= absoluteHeight;

            return withinRadius && withinHeight;
        }
    }
}