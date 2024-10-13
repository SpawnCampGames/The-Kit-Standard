using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace SPWN
{
    /// <summary>
    /// Utility class for common operations.
    /// </summary>
    public static class Utils
    {
        #region Directions
        /// <summary>
        /// Get a direction vector based on the given cardinal direction.
        /// </summary>
        /// <param name="direction">The cardinal direction.</param>
        /// <param name="transform">Optional transform for local space directions.</param>
        /// <returns>A vector representing the specified direction.</returns>
        public static Vector3 RealDirection(this Direction direction, Transform transform = null)
        {
            // Early return for null transform (world space directions)
            if (transform == null)
            {
                return direction switch
                {
                    Direction.North => Vector3.forward,
                    Direction.West => -Vector3.right,
                    Direction.South => -Vector3.forward,
                    Direction.East => Vector3.right,
                    Direction.Up => Vector3.up,
                    Direction.Down => -Vector3.up,
                    _ => Vector3.zero
                };
            }

            // Return based on the transform (local space directions)
            return direction switch
            {
                Direction.North => transform.forward,
                Direction.West => -transform.right,
                Direction.South => -transform.forward,
                Direction.East => transform.right,
                Direction.Up => transform.up,
                Direction.Down => -transform.up,
                _ => Vector3.zero
            };
        }
        #endregion

        #region Input / Debugging
        /// <summary>
        /// Display real-time debug text on screen.
        /// </summary>
        public static void RealtimeDebug(string text, Vector2 position, int fontSize, Color textColor, Color outlineColor, int outlineThickness, int padding)
        {
            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                fontSize = fontSize,
                normal = { textColor = textColor },
                alignment = TextAnchor.UpperLeft
            };

            // Calculate size and adjust for padding/outline
            Vector2 textSize = style.CalcSize(new GUIContent(text));
            float width = textSize.x + 2 * (padding + outlineThickness);
            float height = textSize.y + 2 * (padding + outlineThickness);
            Vector2 adjustedPosition = position - new Vector2((width - textSize.x) / 2, (height - textSize.y) / 2);

            // Draw outline
            DrawOutline(new Rect(adjustedPosition.x, adjustedPosition.y, width, height), outlineColor, outlineThickness);

            // Draw main text
            GUI.Label(new Rect(adjustedPosition.x + padding + outlineThickness, adjustedPosition.y + padding + outlineThickness, textSize.x, textSize.y), text, style);
        }

        private static void DrawOutline(Rect rect, Color color, int thickness)
        {
            Color originalColor = GUI.color;
            GUI.color = color;

            // Draw top, bottom, left, right outlines
            GUI.DrawTexture(new Rect(rect.x - thickness, rect.y - thickness, rect.width + 2 * thickness, thickness), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(rect.x - thickness, rect.y + rect.height, rect.width + 2 * thickness, thickness), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(rect.x - thickness, rect.y - thickness, thickness, rect.height + 2 * thickness), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(rect.x + rect.width, rect.y - thickness, thickness, rect.height + 2 * thickness), Texture2D.whiteTexture);

            GUI.color = originalColor;
        }
        #endregion

        #region Colors
        /// <summary>
        /// Converts a hex string to a Color.
        /// </summary>
        public static Color HexToColor(string hex)
        {
            hex = hex.Replace("#", ""); // Strip '#' if present
            if (hex.Length != 6) return Color.black; // Fallback in case of invalid input

            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            return new Color32(r, g, b, 255); // Always return full opacity
        }
        #endregion

        #region Distance
        /// <summary>
        /// Returns the squared distance between two points.
        /// </summary>
        public static float SquaredDistance(Vector3 a, Vector3 b) => (a - b).sqrMagnitude;

        /// <summary>
        /// Checks if two points are within a specified range.
        /// </summary>
        public static bool IsInRange(this Vector3 a, Vector3 b, float range) => SquaredDistance(a, b) < range * range;
        #endregion
    }

    #region Mouse Input
    /// <summary>
    /// Utility class for mouse input handling.
    /// </summary>
    public static class Mouse
    {
        public static bool Button(int button, bool held = false) =>
            held ? UnityEngine.Input.GetMouseButton(button) : UnityEngine.Input.GetMouseButtonDown(button);

        public static bool Left(bool held = false) => Button(0, held);
        public static bool Right(bool held = false) => Button(1, held);
        public static bool Middle(bool held = false) => Button(2, held);
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