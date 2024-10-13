using UnityEngine;
namespace SPWN
{
    /// <summary>
    /// <para>Custom <c>Dbug</c> Logs make use of Unity's <c>Debug</c> class.</para>
    /// <para>Enhance your <c>Logs</c> with flexible methods to :</para>
    /// <list type="bullet">
    /// <item><description>Highlight</description></item>
    /// <item><description>Organize</description></item>
    /// <item><description>Emphasize</description></item>
    /// </list>
    /// <para>📗For Documentation, see <a href="https://github.com/SpawnCampGames/Documentation/">SPWN DOCS</a>.</para>
    /// </summary>
    /// <remarks>
    /// Version 8.26
    /// </remarks> 
    public static class Dbug
    {
        /// <summary>
        /// Logs a message with optional color and style formatting.
        /// </summary>
        /// <param name="msg">Message to log.</param>
        /// <param name="color">Color of the log in hexadecimal format. Default is white ("#FFFFFF").</param>
        /// <param name="bold">Should the text be bold? Default is false.</param>
        /// <param name="italic">Should the log be italicized? Default is false.</param>
        /// <param name="underline">Should the log be underlined? Default is false.</param>
        /// <param name="strikethrough">Should the log have a strikethrough? Default is false.</param>
        private static void CustomLog(string msg, string color = "#FFFFFF", bool bold = false, bool italic = false, bool underline = false, bool strikethrough = false)
        {
            if (string.IsNullOrEmpty(msg))
            {
                Debug.Log("Log Failed Successfully. Message is empty.");
                return;
            }

            string formattedMsg = msg;

            // Apply styles
            if (bold) formattedMsg = $"<b>{formattedMsg}</b>";
            if (italic) formattedMsg = $"<i>{formattedMsg}</i>";
            if (strikethrough) formattedMsg = $"<s>{formattedMsg}</s>";
            if (underline) formattedMsg = $"<u>{formattedMsg}</u>";

            // Apply color
            formattedMsg = $"<color={color}>{formattedMsg}</color>";

            // ATTN! EXPAND STACK TRACE IN UNITY CONSOLE FOR CLICKING -> ERROR
            Debug.Log(formattedMsg);
        }

        public static void MyLog(string msg, string color = "#FFFFFF", bool bold = false, bool italic = false, bool underline = false, bool strikethrough = false) => CustomLog(msg, color, bold, italic, underline, strikethrough);

#region Default Loggers

        public static void Log(string msg) => CustomLog(msg, "#FFFFFF");
        public static void Error(string msg) => CustomLog($"💀 {msg}", "#FF0000");
        public static void Warning(string msg) => CustomLog($"⚠️ {msg}", "#FFFF00");

#endregion
#region Colored Loggers

        public static void Red(string msg) => CustomLog(msg, "#FF0000");
        public static void Orange(string msg) => CustomLog(msg, "#FFA500");
        public static void Yellow(string msg) => CustomLog(msg, "#FFFF00");
        public static void Green(string msg) => CustomLog(msg, "#00FF00");
        public static void Blue(string msg) => CustomLog(msg, "#00FFFF");
        public static void Indigo(string msg) => CustomLog(msg, "#4B0082");
        public static void Violet(string msg) => CustomLog(msg, "#A349A4"); // OG being #800080

        #endregion
        #region Styled Loggers

        public static void Bold(string msg) => CustomLog(msg, "#FFFFFF", bold: true);
        public static void Italic(string msg) => CustomLog(msg, "#FFFFFF", italic: true);
        public static void Underline(string msg) => CustomLog(msg, "#FFFFFF", underline: true);
        public static void Strikethrough(string msg) => CustomLog(msg, "#FFFFFF", strikethrough: true);

        #endregion
        #region Special Loggers

        // General
        public static void Test(string msg) => CustomLog($"🧪 {msg}", "#C3EF3C");
        public static void Physics(string msg) => CustomLog($"🚀 {msg}", "#83cbff");
        public static void Audio(string msg) => CustomLog($"🔊 {msg}", "#FFFFFF");
        public static void Rendering(string msg) => CustomLog($"🎥 {msg}", "#FFFFFF");
        public static void Security(string msg) => CustomLog($"🛡️ {msg}", "#007bff");

        // To Self
        public static void Note(string msg) => CustomLog($"📌 Note: {msg}", "#FFFFFF");
        public static void Info(string msg) => CustomLog($"🗨️ {msg}", "#FFFFFF");

        // Measurement
        public static void Time(string msg) => CustomLog($"⌛ {msg}", "#ffde85");
        public static void Distance(string msg) => CustomLog($"📏 {msg}", "#ffde85");
        public static void Angle(string msg) => CustomLog($"📐 {msg}", "#ffde85");

        // Actions
        public static void Do(string msg) => CustomLog($"➤ {msg}", "#00fff7");
        public static void Skip(string msg) => CustomLog($"↻ {msg}", "#ffee00");

        // Results
        public static void Hit(string msg) => CustomLog($"💥 {msg}", "#fcd53f");
        public static void Miss(string msg) => CustomLog($"🍂 {msg}", "#fcd53f");
        public static void Result(string msg) => CustomLog($"🔍 {msg}", "#007bff");
        public static void Save(string msg) => CustomLog($"💿 {msg}", "#007bff");

        #endregion
        #region Status Loggers

        public static void Started(string msg) => CustomLog($"🟢 {msg}", "#00d26a");
        public static void Stopped(string msg) => CustomLog($"🛑 {msg}", "#f8312f");
        public static void Success(string msg) => CustomLog($"✅ {msg}", "#00d26a");
        public static void Fail(string msg) => CustomLog($"❌ {msg}", "#f72f5e");

        #endregion
        #region Emphasized - Misc Loggers

        public static void Extra(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;
            Debug.Log($"🕹️ <b>{msg}</b>\n----------------------------");
        }

        // ummmm whats the difference between CustomLog and MyLog?
        #endregion

        public static string ToLog(this string s){
            Debug.Log($"{s}");
            return s;
        }

        /// <summary>
        /// Draws a circle in the Unity Editor using Gizmos.
        /// <para>Must be used in <c>OnDrawGizmos</c> or <c>OnDrawGizmosSelected</c></para>
        /// </summary>
        /// <param name="center">Center of the circle.</param>
        /// <param name="radius">Radius of the circle.</param>
        /// <param name="axis">Axis to draw the circle around</param>
        /// <param name="color">Color of the circle.</param>
        /// <param name="offset">Offset of the circle if specified.</param>
        public static void Circle(Vector3 center, float radius, Vector3 axis, Color color, Vector3 offset = default)
        {
            Gizmos.color = color;

            int segments = 36;
            float angleStep = 360f / segments;

            // find a vector that is not parallel to the axis
            Vector3 perpendicularVector = Vector3.Cross(axis, Vector3.up).normalized;

            // if zero cross try again
            if (perpendicularVector == Vector3.zero)
            {
                perpendicularVector = Vector3.Cross(axis, Vector3.right).normalized;
            }

            // get starting point
            Vector3 previousPoint = center + offset + Quaternion.AngleAxis(0, axis) * (perpendicularVector * radius);

            for (int i = 1; i <= segments; i++)
            {
                float angle = angleStep * i;
                Vector3 currentPoint = center + offset + Quaternion.AngleAxis(angle, axis) * (perpendicularVector * radius);
                Gizmos.DrawLine(previousPoint, currentPoint);
                previousPoint = currentPoint;
            }
        }

        /// <summary>
        /// Draws a line between two points in the Unity Editor using Gizmos.
        /// <para>Must be used in <c>OnDrawGizmos</c> or <c>OnDrawGizmosSelected</c></para>
        /// </summary>
        /// <param name="start">The starting point of the line.</param>
        /// <param name="end">The ending point of the line.</param>
        /// <param name="color">Color of the line.</param>
        public static void Line(Vector3 start, Vector3 end, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(start, end);
        }

        /// <summary>
        /// Draws a ray with an arrowhead to indicate direction in the Unity Editor using Gizmos.
        /// <para>Must be used in <c>OnDrawGizmos</c> or <c>OnDrawGizmosSelected</c></para>
        /// </summary>
        /// <param name="origin">The starting point of the ray.</param>
        /// <param name="direction">The direction of the ray.</param>
        /// <param name="length">The length of the ray.</param>
        /// <param name="color">Color of the ray.</param>
        public static void Ray(Vector3 origin, Vector3 direction, float length, Color color)
        {
            Gizmos.color = color;

            // Draw the ray line
            Vector3 end = origin + direction.normalized * length;
            Gizmos.DrawLine(origin, end);

            // Draw the arrowhead
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 45, 0) * Vector3.forward * 0.2f;
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -45, 0) * Vector3.forward * 0.2f;
            Gizmos.DrawLine(end, end + right);
            Gizmos.DrawLine(end, end + left);
        }

        /// <summary>
        /// Fun little method that will crash the game.
        /// <para>Technically just calls <c>Application.Quit()</c> or,</para>
        /// <para>If in the editor, <c>UnityEditor.EditorApplication.isPlaying = false;</c></para>
        /// </summary>
        /// <param name="msg"></param>
        public static void Crash(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;
            Error(msg);

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}