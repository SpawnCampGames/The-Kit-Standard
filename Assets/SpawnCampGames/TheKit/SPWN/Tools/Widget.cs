using UnityEngine;

namespace SPWN
{
    public class Widget : Singleton<Widget>
    {
        public Texture2D widgetTex;
        protected override void DoAwake() { }
    }
}
