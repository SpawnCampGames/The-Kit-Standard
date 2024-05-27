using UnityEngine;
namespace NEWNAME
{
    public class BreatheEffect : MonoBehaviour
    {
        Vector3 startPos;

        public float amplitude = 0.01f;
        public float period = 0.3f;
        public float minAmplitude = 0.0075f;
        public float minPeriod = 0.3f;


        protected void Start()
        {
            startPos = transform.localPosition;
        }
        protected void Update()
        {
            if(Input.GetKey(KeyCode.LeftControl))
            {
                float theta = Time.timeSinceLevelLoad / minPeriod;
                float distance = minAmplitude * Mathf.Sin(theta);
                transform.localPosition = startPos + Vector3.up * distance;
            }
            else
            {
                float theta = Time.timeSinceLevelLoad / period;
                float distance = amplitude * Mathf.Sin(theta);
                transform.localPosition = startPos + Vector3.up * distance;
            }
        }
    }
}