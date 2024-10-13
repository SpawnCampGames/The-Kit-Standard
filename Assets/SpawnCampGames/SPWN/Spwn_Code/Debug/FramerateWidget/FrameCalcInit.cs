using UnityEngine;
using System.Collections;
using TMPro;

public class FrameCalcInit : MonoBehaviour
{
    [SerializeField] private GameObject targetEnable;
    [SerializeField] private GameObject targetDisable; 

    [Tooltip("How long to wait before enabling the Framerate Widget")]
    [SerializeField] private float delay = 3.0f;
    [SerializeField] private TMP_Text text;

    void Start() => StartCoroutine(WaitAndEnableCoroutine());

    IEnumerator WaitAndEnableCoroutine()
    {
        if (text != null)
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < delay)
            {
                float remainingTime = delay - elapsedTime;
               
                text.text = $"{Mathf.Ceil(remainingTime)}";
                yield return new WaitForSeconds(1.0f);
                elapsedTime += 1.0f;
            }
        }

        targetEnable.SetActive(true);
        targetDisable.SetActive(false);

        this.enabled = false;
    }
}
