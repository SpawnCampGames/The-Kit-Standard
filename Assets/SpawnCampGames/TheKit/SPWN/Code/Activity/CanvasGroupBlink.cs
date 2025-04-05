using UnityEngine;

namespace SPWN
{
    public class CanvasGroupBlink : MonoBehaviour
    {
        CanvasGroup targetCanvasGroup;

        public float onDuration = 1f;
        public float offDuration = 1f;
        public float transitionSpeed = 1f;

        private void OnEnable()
        {
            if(targetCanvasGroup == null) targetCanvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            Blink();
        }

        void Blink()
        {
            float cycleTime = onDuration + offDuration;
            float t = Mathf.PingPong(Time.time * transitionSpeed,cycleTime) / cycleTime;
            targetCanvasGroup.alpha = Mathf.Lerp(0,1,t);
        }
    }
}
