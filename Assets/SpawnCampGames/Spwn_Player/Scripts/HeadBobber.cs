using UnityEngine;
using System.Collections;

namespace SPWN
{
    public class HeadBobber : MonoBehaviour
    {
        SpawnCampController spwnCont;
        public float length = 1f;
        public float upDuration = 1f; 
        public float downDuration = 1f;

        bool standby = false;

        Vector3 originalPosition;
        Vector3 targetUpPosition;
        Vector3 targetDownPosition;

        void Start()
        {
            spwnCont = FindFirstObjectByType<SpawnCampController>();
            originalPosition = transform.localPosition;
            targetUpPosition = originalPosition + (transform.up * length);
            targetDownPosition = originalPosition;
        }

        public void CamRebound()
        {
            if(!standby)
            {
                StartCoroutine(Rebound());
            }
        }

        IEnumerator Rebound()
        {
            standby = true;
            float elapsedTime = 0;
            Vector3 startingPos = transform.localPosition;

            while(elapsedTime < upDuration)
            {
                transform.localPosition = Vector3.Lerp(startingPos,targetUpPosition,(elapsedTime / upDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = targetUpPosition;

            elapsedTime = 0;
            startingPos = transform.localPosition;

            while(elapsedTime < downDuration)
            {
                transform.localPosition = Vector3.Lerp(startingPos,targetDownPosition,(elapsedTime / downDuration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = targetDownPosition;
            standby = false;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                CamRebound();
            }
        }
    }
}
