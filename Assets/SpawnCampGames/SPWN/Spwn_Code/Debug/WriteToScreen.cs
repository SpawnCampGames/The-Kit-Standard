using UnityEngine;
using SPWN;

public class WriteOnScreen : MonoBehaviour
{
    public string msg = "THE-KIT - v0.0.1 - 2024";
    public Color textColor = Color.cyan;

    [HideInInspector]public int textSize = 24;
    [HideInInspector]public Vector2 screenSize = new Vector2(1000, 200);
    [HideInInspector]public Vector2 offset = new Vector2(40, 40);

    private void OnGUI() {
           Utils.RealtimeDebug(msg, offset, textSize, textColor, screenSize);
    }
}
