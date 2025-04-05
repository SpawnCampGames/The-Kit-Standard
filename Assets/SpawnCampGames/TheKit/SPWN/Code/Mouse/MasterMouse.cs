using UnityEngine;
using SPWN;

public class MasterMouse : Singleton<MasterMouse>
{
    [Header("INITIALIZATION")]
    [Tooltip("Dont change after game started")]
    [SerializeField] bool locked;
    [Tooltip("Dont change after game started")]
    [SerializeField] bool confined;
    [Tooltip("Dont change after game started")]
    [SerializeField] bool hidden;

    [Header("Key Bindings")]
    public KeyCode HideCursorKey = KeyCode.Keypad1;
    public KeyCode UnhideCursorKey = KeyCode.Keypad4;
    public KeyCode LockCursorKey = KeyCode.Keypad2;
    public KeyCode UnlockCursorKey = KeyCode.Keypad5;
    public KeyCode ConfineCursorKey = KeyCode.Keypad3;
    public KeyCode UnconfineCursorKey = KeyCode.Keypad6;

    //dont use Awake() or you'll break Instancing
    protected override void DoAwake()
    {
        Initialization();
        Dbug.Log("MasterMouse Initialized");
    }

    private void Initialization()
    {
        if(confined)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if(locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
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
        Dbug.Log($"Cursor Locked: {Cursor.lockState}");
    }

    public void UnlockCursor()
    {
        if(confined)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

        Dbug.Log($"Cursor Unlocked: {Cursor.lockState}");
    }

    public void ConfineCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Dbug.Log("Cursor Confined to Game Window");
    }

    public void UnconfineCursor()
    {
        Cursor.lockState = CursorLockMode.None;

        if(locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

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
