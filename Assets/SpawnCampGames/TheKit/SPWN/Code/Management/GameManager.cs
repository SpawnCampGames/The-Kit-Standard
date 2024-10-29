using UnityEngine;
using System.Collections;

using SPWN;

public class GameManager : Singleton<GameManager>
{
  // important variables
  SpawnCampControllerLite myController;
  ControllerCamera myCameraController;

  void Start()
  {
    // get player
    myController = FindFirstObjectByType<SpawnCampControllerLite>();
    myCameraController = FindFirstObjectByType<ControllerCamera>();

    // disable player
    myController.enabled = false;
    myCameraController.enabled = false;

    Dbug.Test("SPWN.Dbug has started");
    StartCoroutine(WaitAndPlay());
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.X))
    {
      EndGame();
    }
  }

  void StartGame()
  {
    Dbug.Started("Game has started");

    // enable player
    myCameraController.enabled = true;
    myController.enabled = true;
  }

  void EndGame()
  {
    // disable player
    myCameraController.enabled = false;
    myController.enabled = false;

    Dbug.Stopped("Game has ended");
  }

  private IEnumerator WaitAndPlay()
  {
    yield return new WaitForSeconds(1f);
    StartGame();
  }
}
