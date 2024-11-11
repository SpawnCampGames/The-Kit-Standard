using UnityEngine;
using UnityEditor;
using SPWN;
using System.Collections;

/// <summary>
/// Diagnostics and Demonstration for <c>SPWN.Dbug</c>, Custom Debugging Class.
/// <para>Add to any GameObject in your scene.</para>
/// <para><c>OnEnable()</c> will automatically begin <c>Dbug</c> Burn-in Diagnostics.</para>
/// <para>* View the console for output.</para>
/// <See cref="Dbug"/>
/// <para>For documentation, see <a href="https://github.com/SpawnCampGames/TheKit/Documentation/">SPWN DOCS</a>.</para>
/// </summary>
/// <remarks>
/// Version 8.26
/// </remarks>
public class DbugDiagnostics : MonoBehaviour
{
    private WaitForSecondsRealtime OneSec = new WaitForSecondsRealtime(1f);
    private WaitForSecondsRealtime HalfSec = new WaitForSecondsRealtime(.5f);
    private WaitForSecondsRealtime FractionSec = new WaitForSecondsRealtime(.1f);

    [MenuItem("SpawnCampGames/Debug/Dbug Diagnostics", false, 17)]
    public static void StartColorBurnIn()
    {
        DbugDiagnostics diagnosticsInstance = FindFirstObjectByType<DbugDiagnostics>();

        if (diagnosticsInstance != null)
        {
            if (Application.isPlaying)
            {
                diagnosticsInstance.StartCoroutine(diagnosticsInstance.BurnInCoroutine());
            }else
            {
                Debug.LogError("Cannot start burn-in diagnostics in edit mode.");
            }
        }
        else
        {
            Debug.LogError("No Dbug Diagnostics instance found in the scene.");
        }
    }

    private IEnumerator BurnInCoroutine()
    {
        // Initial warm-up message
        Dbug.Extra("SPWN Dbug Warming up. This will take a few seconds.");
        yield return OneSec;

        // title log (wait 1 second)
        Dbug.Extra("Burn-In Sequence 1 Beginning. [Default Dbug Logs]");
        yield return OneSec;

        Dbug.Log("This is a regular log message.");
        yield return HalfSec;

        Dbug.Error("This is an error message.");
        yield return HalfSec;

        Dbug.Warning("This is a warning message.");
        yield return HalfSec;

        // title log (wait 1 second)
        Dbug.Extra("Burn-In Sequence 2 Beginning. [Styled Dbug Logs]");
        yield return OneSec;

        Dbug.Extra("This is an emphasized message.");
        yield return HalfSec;

        Dbug.Bold("This is a bold message.");
        yield return HalfSec;

        Dbug.Italic("This is an italic message.");
        yield return HalfSec;

        Dbug.Underline("This is an underlined message.");
        yield return HalfSec;

        Dbug.Strikethrough("This is a strikethrough message.");
        yield return HalfSec;

        // title log (wait 1 second)
        Dbug.Extra("Burn-In Sequence 3 Beginning. [Colored Dbug Logs]");
        yield return OneSec;

        yield return ColorCalibration();

        // title log (wait 1 second)
        Dbug.Extra("Burn-In Sequence 4 Beginning. [Special Dbug Logs]");
        yield return OneSec;

        Dbug.Physics("This is a Physics message.");
        yield return HalfSec;

        Dbug.Test("This is a Test message.");
        yield return HalfSec;

        Dbug.Info("This is an Info message.");
        yield return HalfSec;

        Dbug.Hit("This is a Physics Hit-Collision message.");
        yield return HalfSec;

        // title log (wait 1 second)
        Dbug.Extra("Burn-In Sequence 5 Beginning. [Status Dbug Logs]");
        yield return OneSec;

        Dbug.Started("This is a Started message.");
        yield return HalfSec;

        Dbug.Stopped("This is a Stopped message.");
        yield return HalfSec;

        Dbug.Success("This is a Succeeded message.");
        yield return HalfSec;

        Dbug.Fail("This is a Failed message.");
        yield return HalfSec;

        // title log (wait 1 second)
        Dbug.Extra("Final Burn-In Sequence. [Countdown to Crash()]");
        yield return OneSec;

        // Countdown to crash
        Dbug.Extra("Crashing in 3...");
        yield return OneSec;

        Dbug.Extra("2...");
        yield return OneSec;

        Dbug.Extra("1...");
        yield return OneSec;

        Crash(); // Force Crash (only works in Editor)
    }

    private IEnumerator ColorCalibration()
    {
        string baseMessage = "Burn-In Color Calibration";
        int maxDots = 5;
        string CreateDotProgress(int dotCount) => new string('.', dotCount);

        for (int dotCount = 1; dotCount <= maxDots; dotCount++)
        {
            string message = $"{baseMessage}{CreateDotProgress(dotCount)}";
            CallColorMethod(dotCount, message);
            yield return FractionSec;
        }

        for (int dotCount = maxDots - 1; dotCount > 0; dotCount--)
        {
            string message = $"{baseMessage}{CreateDotProgress(dotCount)}";
            CallColorMethod(dotCount, message);
            yield return FractionSec;
        }
    }

    private void CallColorMethod(int index, string message)
    {
        switch (index % 12)
        {
            case 0: Dbug.Red(message); break;
            case 1: Dbug.Orange(message); break;
            case 2: Dbug.Yellow(message); break;
            case 3: Dbug.Green(message); break;
            case 4: Dbug.Blue(message); break;
            case 5: Dbug.Indigo(message); break;
            case 6: Dbug.Violet(message); break;
            case 7: Dbug.Indigo(message); break;
            case 8: Dbug.Blue(message); break;
            case 9: Dbug.Green(message); break;
            case 10: Dbug.Yellow(message); break;
            case 11: Dbug.Orange(message); break;
            default: Dbug.Red(message); break;
        }
    }

    public void Crash()
    {
        if (Application.isPlaying)
        {
            Dbug.Crash("Something went wrong!");
        }
    }
}