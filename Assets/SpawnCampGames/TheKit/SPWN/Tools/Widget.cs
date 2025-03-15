using UnityEngine;
namespace SPWN
{
    public class Widget : Singleton<Widget>
    {
        public Texture2D widgetTex; // hidden by WidgetEditor > Use debug view of inspector for access
        protected override void DoAwake() { }
    }
}
