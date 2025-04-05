using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    bool isGamePaused = false;
    public KeyCode PauseKey = KeyCode.Escape;
    public GameObject pauseCanvas;

    private void Update()
    {
        if(Input.GetKeyDown(PauseKey))
        {
            if(isGamePaused)
            {
                //game is paused, lets unpause it
                MasterMouse.Instance.HideCursor();
                MasterMouse.Instance.LockCursor();

                //disable the pause menu
                pauseCanvas.SetActive(false);

                //mark our system as unpaused
                isGamePaused = false;

                Debug.Log("We've successfully unpaused the game.");
            }
            else
            {
                //game is unpaused, lets pause it
                MasterMouse.Instance.UnhideCursor();
                MasterMouse.Instance.UnlockCursor();

                //enable the pause menu
                pauseCanvas.SetActive(true);

                //mark our system as paused
                isGamePaused = true;
                Debug.Log("We've successfully paused the game.");
            }
        }
    }
}
