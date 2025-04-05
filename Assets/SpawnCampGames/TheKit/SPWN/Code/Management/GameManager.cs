using UnityEngine;
using System.Collections;

using SPWN;

public class GameManager : Singleton<GameManager>
{
  // important variables

  void Start()
  {
    Dbug.Test("SPWN.Dbug has started");
    StartCoroutine(WaitAndPlay());
  }

  void StartGame()
  {
    Dbug.Started("Game has started");

    // enable player
    // myCameraController.enabled = true;
    // myController.enabled = true;
  }

  void EndGame()
  {
    // disable player
    // myCameraController.enabled = false;
    // myController.enabled = false;
    Dbug.Stopped("Game has ended");
  }

  private IEnumerator WaitAndPlay()
  {
    yield return new WaitForSeconds(1f);
    StartGame();
  }
}
