using SPWN;
using System;
using UnityEngine;

public class DebugHotBar : MonoBehaviour
{
    [Header("DBUG DATA")]
    public string DBUGDATA = "HOLD F1 AND PRESS 0-9 ALPHA KEYS";
    void Update()
    {
        // Check if F1 is being held down
        if(Input.GetKey(KeyCode.F1))
        {
            // Loop through keys 0-9 and handle with a switch statement
            for(int i = 0; i <= 9; i++)
            {
                if(Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode),$"Alpha{i}")))
                {
                    HandleKeyPress(i);
                }
            }
        }
    }

    void HandleKeyPress(int keyIndex)
    {
        switch(keyIndex)
        {
            case 0:
            DbugGameFeed.Instance.UpdateDisplay("HOTKEY 0"); break;

            case 1:
            DbugGameFeed.Instance.UpdateDisplay("HOTKEY 1"); break;

            case 2:
            DbugGameFeed.Instance.UpdateDisplay("HOTKEY 2"); break;

            case 3:
            DbugGameFeed.Instance.UpdateDisplay("HOTKEY 3"); break;

            case 4:
            DbugGameFeed.Instance.UpdateDisplay("HOTKEY 4"); break;

            case 5:
            DbugGameFeed.Instance.UpdateDisplay("HOTKEY 5"); break;

            case 6:
            DbugGameFeed.Instance.UpdateDisplay("HOTKEY 6"); break;

            case 7:
            DbugGameFeed.Instance.UpdateDisplay("HOTKEY 7"); break;

            case 8:
            DbugGameFeed.Instance.UpdateDisplay("HOTKEY 8"); break;

            case 9:
            DbugGameFeed.Instance.UpdateDisplay("HOTKEY 9"); break;

            default:
            DbugGameFeed.Instance.UpdateDisplay("NOT FOUND"); break;
        }
    }

}

