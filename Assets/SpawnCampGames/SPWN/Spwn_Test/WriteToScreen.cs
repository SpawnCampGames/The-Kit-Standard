using UnityEngine;
using SPWN;

public class WriteOnScreen : MonoBehaviour
{
    public string testString = "THE-KIT";

    private void OnGUI() {
           Utils.RealtimeDebug(testString, new Vector2(10, 10), new Vector2(500, 200), Color.cyan);
    }
}
