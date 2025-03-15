using UnityEngine;
using SPWN;

public class MasterMouse : Singleton<MasterMouse>
{
    [Header("INITIALIZATION")]
    [SerializeField] bool locked;
    [SerializeField] bool confined;
    [SerializeField] bool hidden;

    [Header("Key Bindings")]
    public KeyCode HideCursorKey = KeyCode.Keypad1;
    public KeyCode UnhideCursorKey = KeyCode.Keypad4;
    public KeyCode LockCursorKey = KeyCode.Keypad2;
    public KeyCode UnlockCursorKey = KeyCode.Keypad5;
    public KeyCode ConfineCursorKey = KeyCode.Keypad3;
    public KeyCode UnconfineCursorKey = KeyCode.Keypad6;

    private void Awake()
    {
        Dbug.Log("MasterMouse Initialized");
        Cursor.visible = !hidden;
        Initialization();
    }

    private void Initialization()
    {
        if(locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if(confined)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        Cursor.visible = !hidden;
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Dbug.Log("Cursor Hidden");
    }

    public void UnhideCursor()
    {
        Cursor.visible = true;
        Dbug.Log("Cursor Unhidden");
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Dbug.Log("Cursor Locked");
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Dbug.Log("Cursor Unlocked");
    }

    public void ConfineCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Dbug.Log("Cursor Confined to Game Window");
    }

    public void UnconfineCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Dbug.Log("Cursor Unconfined");
    }

    void Update()
    {
        // Listen for input and toggle cursor states accordingly
        if(Input.GetKeyDown(HideCursorKey)) HideCursor();
        if(Input.GetKeyDown(UnhideCursorKey)) UnhideCursor();
        if(Input.GetKeyDown(LockCursorKey)) LockCursor();
        if(Input.GetKeyDown(UnlockCursorKey)) UnlockCursor();
        if(Input.GetKeyDown(ConfineCursorKey)) ConfineCursor();
        if(Input.GetKeyDown(UnconfineCursorKey)) UnconfineCursor();
    }
}
