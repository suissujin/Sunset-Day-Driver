using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public PlayerCarController carController;
    public bool gamePaused = false;
    public void Pause()
    {
        if (gamePaused)
        {
            Time.timeScale = 1;
            gamePaused = false;
            carController.quitCounter = 0;
            Debug.Log("Game Unpaused");
        }
        else
        {
            Time.timeScale = 0;
            gamePaused = true;
            Debug.Log("Game Paused");
        }
    }
}
