using UnityEngine;

namespace SPWN // SpawnCampGames.com
{
    /// <summary>
    /// Utility class for calculating global transformation data of a GameObject and its parent hierarchy.
    /// </summary>
    public static class GlobalTransform
    {
        /// <summary>
        /// Calculates the global position, rotation, and scale of a transform.
        /// </summary>
        /// <param name="core">The transform to calculate the global transformation for.</param>
        /// <param name="globalPosition">The global position of the transform.</param>
        /// <param name="globalRotation">The global rotation of the transform in Euler angles.</param>
        /// <param name="globalScale">The global scale of the transform.</param>
        public static void GetGlobalTransform(Transform core, out Vector3 globalPosition, out Vector3 globalRotation, out Vector3 globalScale)
        {
            // Set the global position and rotation
            globalPosition = core.position;
            globalRotation = core.rotation.eulerAngles;

            // Calculate the global scale manually
            globalScale = CalculateGlobalScale(core);
        }

        /// <summary>
        /// Calculates the global scale of a transform considering its parent hierarchy.
        /// </summary>
        /// <param name="core">The transform to calculate the global scale for.</param>
        /// <returns>The global scale of the transform.</returns>
        private static Vector3 CalculateGlobalScale(Transform core)
        {
            Vector3 scale = core.localScale;
            Transform currentCore = core.parent;

            // Iterate through parent transforms, multiplying their scales
            while (currentCore != null)
            {
                scale.Scale(currentCore.localScale);
                currentCore = currentCore.parent;
            }

            return scale;
        }
    }
}
