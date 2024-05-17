using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class FrameCalculator : MonoBehaviour
{
    // TEXT VARIABLES
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text frameRateText;
    [SerializeField] private TMP_Text avgFrameRateText;
    [SerializeField] private TMP_Text lowestFrameRateText;

    private float fps = 0.0f;
    private float lowestFrameRate = float.MaxValue;
    private float averageFrameRate = 0.0f;

    private float averageDuration = 5.0f; // AVERAGE FRAMERATE LAP TIME
    private bool averageCalculated = false;

    private Queue<float> frameCounts = new Queue<float>();
    private float totalTime = 0.0f;

    // PING PONG VARIABLES
    [SerializeField] private Image pingPongBlip;
    public float pingPongSpeed = 100f;
    public float pingPongMin = 0f;
    public float pingPongMax = 550f;

    private float pingPongPosition;
    private Vector2 pingPongStartingPosition;
    private Vector2 pingPongEndingPosition;

    void Start()
    {
        // Get starting and ending positions
        pingPongStartingPosition = pingPongBlip.rectTransform.anchoredPosition;
        pingPongEndingPosition = pingPongStartingPosition + Vector2.right * (pingPongMax - pingPongMin);
    }

    void Update()
    {
        // Calculate ping-pong position using Mathf.PingPong function
        pingPongPosition = Mathf.PingPong(Time.time * pingPongSpeed,pingPongMax - pingPongMin) + pingPongMin;

        // Update anchored position of the pingPongBlip
        pingPongBlip.rectTransform.anchoredPosition = Vector2.Lerp(pingPongStartingPosition,pingPongEndingPosition,pingPongPosition / (pingPongMax - pingPongMin));

        // CALCULATE ONLY IF TIME PASSING
        float dt = Time.deltaTime;
        if(dt > 0.0f)
        {
            totalTime += dt;
            fps = 1.0f / dt;

            // REALTIME FRAMERATE (RT)
            frameRateText.text = "REAL: " + fps.ToString("F0");

            // AVERAGE FRAMERATE (ROLLING)
            frameCounts.Enqueue(fps); // ADD CURRENT
            if(totalTime >= averageDuration)
            {
                // REMOVE OLD
                while(totalTime - frameCounts.Peek() >= averageDuration)
                {
                    frameCounts.Dequeue();
                }

                // CALCULATE
                float sum = 0.0f;
                foreach(float frameCount in frameCounts)
                {
                    sum += frameCount;
                }
                averageFrameRate = sum / frameCounts.Count;
                avgFrameRateText.text = "ROLL: " + averageFrameRate.ToString("F0");
                averageCalculated = true;
            }

            // UPDATE LOWEST FRAME RATE AFTER AVERAGE IS CALCULATED
            if(averageCalculated && fps < lowestFrameRate)
            {
                lowestFrameRate = fps;
                lowestFrameRateText.text = "LOW: " + lowestFrameRate.ToString("F0");
            }
        }

        // DISPLAY TIME
        timeText.text = "TIME: " + Time.time.ToString("F4");
    }
}
