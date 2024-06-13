using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class FrameCalculator : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text frameRateText;
    [SerializeField] TMP_Text avgFrameRateText;
    [SerializeField] TMP_Text lowestFrameRateText;

    float fps = 0.0f;
    float lowestFrameRate = float.MaxValue;
    float averageFrameRate = 0.0f;

    float averageDuration = 5.0f; // Average framerate lap time
    bool averageCalculated = false;

    Queue<float> frameCounts = new Queue<float>();
    float totalTime = 0.0f;

    [SerializeField] Image pingPongBlip;
    public float pingPongSpeed = 100f;
    public float pingPongMin = 0f;
    public float pingPongMax = 550f;

    float pingPongPosition;
    Vector2 pingPongStartingPosition;
    Vector2 pingPongEndingPosition;

    void Start()
    {
        // get starting and ending positions for ping pong animation
        pingPongStartingPosition = pingPongBlip.rectTransform.anchoredPosition;
        pingPongEndingPosition = pingPongStartingPosition + Vector2.right * (pingPongMax - pingPongMin);
    }

    void Update()
    {
        // calculate ping-pong position
        pingPongPosition = Mathf.PingPong(Time.time * pingPongSpeed,pingPongMax - pingPongMin) + pingPongMin;
        pingPongBlip.rectTransform.anchoredPosition = Vector2.Lerp(pingPongStartingPosition,pingPongEndingPosition,pingPongPosition / (pingPongMax - pingPongMin));

        // calculate framerate only if time is passing
        float dt = Time.deltaTime;
        if(dt > 0.0f)
        {
            totalTime += dt;
            fps = 1.0f / dt;

            frameRateText.text = "REAL : " + fps.ToString("F0").PadLeft(7,'0');

            // rolling average framerate
            frameCounts.Enqueue(fps);
            if(totalTime >= averageDuration)
            {
                while(totalTime - frameCounts.Peek() >= averageDuration)
                {
                    frameCounts.Dequeue();
                }

                float sum = 0.0f;
                foreach(float frameCount in frameCounts)
                {
                    sum += frameCount;
                }
                averageFrameRate = sum / frameCounts.Count;
                avgFrameRateText.text = "ROLL : " + averageFrameRate.ToString("F0").PadLeft(7,'0');
                averageCalculated = true;
            }

            // update lowest framerate after average is calculated
            if(averageCalculated && fps < lowestFrameRate)
            {
                lowestFrameRate = fps;
                lowestFrameRateText.text = "LOW : " + lowestFrameRate.ToString("F0").PadLeft(7,'0');
            }
        }

        // display time
        timeText.text = "TIME : " + Time.time.ToString("F4").PadLeft(7,'0');
    }
}
