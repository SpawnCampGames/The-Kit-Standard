namespace SPWN
{
    /// <summary>
    /// Utility class for SpawnCampGames.
    /// </summary>
    public static class Utils
    {

    }

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
}
