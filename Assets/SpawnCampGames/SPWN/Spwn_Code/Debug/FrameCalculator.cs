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
        // Get starting and ending positions for ping pong animation
        pingPongStartingPosition = pingPongBlip.rectTransform.anchoredPosition;
        pingPongEndingPosition = pingPongStartingPosition + Vector2.right * (pingPongMax - pingPongMin);
    }

    void Update()
    {
        // Calculate the length of one ping-pong cycle
        float cycleLength = pingPongMax - pingPongMin;

        // Convert pingPongSpeed to units per second
        float unitsPerSecond = pingPongSpeed * cycleLength * 2; // Multiply by 2 because ping-pong goes back and forth

        // Calculate ping-pong position
        pingPongPosition = Mathf.PingPong(Time.time * unitsPerSecond, cycleLength) + pingPongMin;
        pingPongBlip.rectTransform.anchoredPosition = Vector2.Lerp(pingPongStartingPosition, pingPongEndingPosition, pingPongPosition / cycleLength);

        // Calculate framerate only if time is passing
        float dt = Time.deltaTime;
        if (dt > 0.0f)
        {
            totalTime += dt;
            fps = 1.0f / dt;

            frameRateText.text = "REAL : " + fps.ToString("F0").PadLeft(7, '0');

            // Rolling average framerate
            frameCounts.Enqueue(fps);

            // Check if the queue has items before processing
            while (frameCounts.Count > 0 && totalTime - frameCounts.Peek() >= averageDuration)
            {
                frameCounts.Dequeue();
            }

            if (frameCounts.Count > 0)
            {
                float sum = 0.0f;
                foreach (float frameCount in frameCounts)
                {
                    sum += frameCount;
                }
                averageFrameRate = sum / frameCounts.Count;
                avgFrameRateText.text = "ROLL : " + averageFrameRate.ToString("F0").PadLeft(7, '0');
                averageCalculated = true;
            }

            // Update lowest framerate after average is calculated
            if (averageCalculated && fps < lowestFrameRate)
            {
                lowestFrameRate = fps;
                lowestFrameRateText.text = "LOW : " + lowestFrameRate.ToString("F0").PadLeft(7, '0');
            }
        }

        // Display time
        timeText.text = "TIME : " + Time.time.ToString("F4").PadLeft(7, '0');
    }
}
