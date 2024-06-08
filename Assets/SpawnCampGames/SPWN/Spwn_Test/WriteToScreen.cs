using UnityEngine;
using SPWN;

public class WriteOnScreen : MonoBehaviour
{
    [HideInInspector]public string msg = "THE-KIT - v0.0.1 - 2024";
    [HideInInspector]public int textSize = 24;
    [HideInInspector]public float offset = 40;
    [HideInInspector]public float offset2 = 40;

    private void OnGUI() {
           Utils.RealtimeDebug(msg, new Vector2(offset,offset2), textSize, Color.cyan, new Vector2(1000, 200));
    }
}
