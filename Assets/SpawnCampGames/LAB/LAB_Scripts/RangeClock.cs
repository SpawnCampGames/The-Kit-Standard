using UnityEngine;
using System.Collections;
using TMPro;

/// <summary>
/// <para><c>RangeClock</c> script for displaying time, countdowns, and special effects.</para>
/// <para>Supports the following modes:</para>
/// <list type="bullet">
/// <item><b>Clock Mode</b> Displays the current time in HH:mm format.</item>
/// <item><b>Meltdown Mode</b> Initiates a rapid countdown from 9999.</item>
/// <item><b>Countdown Mode</b> Counts down from a specified number.</item>
/// <item><b>Scramble Effect</b> Shows a flashing sequence before revealing a target number.</item>
/// </list>
/// </summary>
/// <remarks>
/// Included in: The L.A.B.
/// </remarks>
public class RangeClock : MonoBehaviour
{
    [Header("Countdown Settings")]
    public int CountdownStart = 5;

    private TextMeshPro clockText;
    private Coroutine activeRoutine;

    // Clock mode
    private bool isClockMode = true;

    // Meltdown mode variables
    private bool isMeltdownActive = false;
    private float meltdownTimer;
    private const int startMeltdownNumber = 9999;

    private void Start()
    {
        clockText = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if(isClockMode)
            UpdateClock();

        if(isMeltdownActive)
            PerformMeltdownCountdown();
    }

    // Update the clock in HH:mm format
    private void UpdateClock()
    {
        System.DateTime now = System.DateTime.Now;
        int hours = now.Hour % 12;
        hours = hours == 0 ? 12 : hours;
        int minutes = now.Minute;

        clockText.text = $"{hours:00}{minutes:00}";
    }

    public void DropCoroutines()
    {
        if(activeRoutine != null)
            StopCoroutine(activeRoutine);
    }

    public void TurnOnClock(bool withMeltdown)
    {
        DropCoroutines();
        if(withMeltdown)Meltdown(1f);
        isClockMode = true;
    }

    public void TurnOffClock(bool withMeltdown)
    {
        DropCoroutines();
        if(withMeltdown) Meltdown(1f);
        isClockMode = false;
    }

    // Start the Meltdown countdown
    public void Meltdown(float duration)
    {
        if(activeRoutine != null)
            StopCoroutine(activeRoutine);

        isMeltdownActive = true;
        meltdownTimer = duration;
        clockText.text = $"{startMeltdownNumber:0000}";
    }

    // Perform the countdown for Meltdown mode
    private void PerformMeltdownCountdown()
    {
        if(meltdownTimer > 0)
        {
            meltdownTimer -= Time.deltaTime;
            int currentNumber = Mathf.FloorToInt(startMeltdownNumber - (startMeltdownNumber * (1 - meltdownTimer / 2f)));

            //we need to check if its negative before updating it
            if(currentNumber > 0)
            {
                clockText.text = $"{currentNumber:0000}";
            }
        }
        else
        {
            isMeltdownActive = false;
            clockText.text = "0000";
        }
    }

    // Start the countdown between start and end numbers
    public void StartCountdownBySecond(int startNumber,int endNumber)
    {
        if(isMeltdownActive)
            return;

        TurnOffClock(false);
        activeRoutine = StartCoroutine(CountdownSteps(startNumber,endNumber));
    }

    private IEnumerator CountdownSteps(int start,int end)
    {
        for(int i = start; i >= end; i--)
        {
            clockText.text = $"{i:0000}";
            yield return new WaitForSeconds(1f);
        }
    }

    // Set a specific number with scrambling visual effect
    public void SetNumber(int number,float duration)
    {
        ScrambleToNumber(number,duration);
    }

    // Generate a random number and show with scrambling effect
    public void GenerateRandomNumber(float duration)
    {
        int randomValue = Random.Range(0,10000);
        ScrambleToNumber(randomValue,duration);
    }

    // Start scrambling effect before showing the target number
    private void ScrambleToNumber(int targetNumber,float duration)
    {
        TurnOffClock(false);
        activeRoutine = StartCoroutine(ScrambleEffect(targetNumber,duration));
    }

    // Scramble effect that flashes random numbers before showing the final number
    private IEnumerator ScrambleEffect(int targetNumber,float duration)
    {
        float scrambleTime = duration * 0.7f;
        float flashTime = duration * 0.3f;

        float scrambleEndTime = Time.time + scrambleTime;
        while(Time.time < scrambleEndTime)
        {
            clockText.text = $"{Random.Range(0,10000):0000}";
            yield return new WaitForSeconds(0.05f);
        }

        // Flash the final number a few times
        for(int i = 0; i < 3; i++)
        {
            clockText.text = "----";
            yield return new WaitForSeconds(flashTime / 6);
            clockText.text = $"{targetNumber:0000}";
            yield return new WaitForSeconds(flashTime / 6);
        }

        clockText.text = $"{targetNumber:0000}";
    }

    #region Test Methods

    [ContextMenu("Clock on")]
    public void TestMeth_ClockMode()
    {
        TurnOnClock(true);
    }

    [ContextMenu("Clock off")]
    public void TestMeth_ClockModeFalse()
    {
        TurnOffClock(true);
    }

    [ContextMenu("Reset")]
    public void TestMeth_Meltdown()
    {
        Meltdown(1f);
    }

    [ContextMenu("Normal Countdown")]
    public void TestMeth_Countdown()
    {
        StartCountdownBySecond(CountdownStart,0);
    }

    [ContextMenu("Set Number")]
    public void TestMeth_SetNumber()
    {
        SetNumber(8008,1.5f);
    }

    [ContextMenu("Random Number")]
    public void TestMeth_RandomNumber()
    {
        SetNumber(Random.Range(1000,10000),1.5f);
    }

    #endregion
}
