using UnityEngine;

namespace SPWN
{
    /// <summary>
    /// Utility class for SpawnCampGames.
    /// </summary>
    public static class Utils
    {

        #region Directions

        /// <summary>
        /// Get a direction vector corresponding to the specified cardinal direction.
        /// </summary>
        /// <param name="direction">The cardinal direction.</param>
        /// <returns>A vector representing the specified direction.</returns>
        public static Vector3 GetDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Vector3.forward;
                case Direction.West:
                    return -Vector3.right;
                case Direction.South:
                    return -Vector3.forward;
                case Direction.East:
                    return Vector3.right;
                case Direction.Up:
                    return Vector3.up;
                case Direction.Down:
                    return -Vector3.up;
                default:
                    return Vector3.zero;
            }
        }

        /// <summary>
        /// Get a direction vector corresponding to the specified local direction relative to a transform.
        /// </summary>
        /// <param name="transform">The transform defining the local space.</param>
        /// <param name="localDirection">The local direction.</param>
        /// <returns>A vector representing the specified local direction.</returns>
        public static Vector3 GetDirection(Direction localDirection, Transform transform)
        {
            switch (localDirection)
            {
                case Direction.North:
                    return transform.forward;
                case Direction.West:
                    return -transform.right;
                case Direction.South:
                    return -transform.forward;
                case Direction.East:
                    return transform.right;
                case Direction.Up:
                    return transform.up;
                case Direction.Down:
                    return -transform.up;
                default:
                    return Vector3.zero;
            }
        }

        #endregion

        #region Input / Debugging
        /// <summary>
        /// Display text on the screen in real-time for debugging purposes.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="position">The position of the text on the screen.</param>
        /// <param name="size">The size of the text.</param>
        /// <param name="textColor">The color of the text.</param>
        public static void RealtimeDebug(string text, Vector2 position, Vector2 size, Color textColor)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.normal.textColor = textColor;

            Rect rect = new Rect(position.x, position.y, size.x, size.y);
            GUI.Label(rect, text, style);
        }
        #endregion
    }

    #region Mouse Input

    /// <summary>
    /// Utility class providing access to mouse input states.
    /// </summary>
    public static class Mouse
    {
        public static bool Left(bool held = false)
        {
            if(held)
            {
                return UnityEngine.Input.GetMouseButton(0); // Returns true while the left mouse button is held down
            }
            else
            {
                return UnityEngine.Input.GetMouseButtonDown(0); // Returns true during the frame the left mouse button is pressed down
            }
        }

        public static bool Right(bool held = false)
        {
            if(held)
            {
                return UnityEngine.Input.GetMouseButton(1); // Returns true while the right mouse button is held down
            }
            else
            {
                return UnityEngine.Input.GetMouseButtonDown(1); // Returns true during the frame the right mouse button is pressed down
            }
        }

        public static bool Middle(bool held = false)
        {
            if(held)
            {
                return UnityEngine.Input.GetMouseButton(2); // Returns true while the middle mouse button is held down
            }
            else
            {
                return UnityEngine.Input.GetMouseButtonDown(2); // Returns true during the frame the middle mouse button is pressed down
            }
        }
    }

    #endregion

    #region Enums
    /// <summary>
    /// Type of Physical Switch
    /// </summary>
    public enum SwitchType { None, Single, Repetitive, Toggle }

    /// <summary>
    /// Cardinal Directions
    /// </summary>
    public enum Direction { North, West, South, East, Up, Down }

    /// <summary>
    /// Basic Axi
    /// </summary>
    public enum Axis { X, Y, Z }
    
    #endregion
}
