using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        }
        else
        {
            Time.timeScale = 0;
            gamePaused = true;
            Gamepad.current?.SetMotorSpeeds(0, 0);
        }
    }
}
