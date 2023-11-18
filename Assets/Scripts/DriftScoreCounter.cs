using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class DriftScoreCounter : MonoBehaviour
{
    public DriftCheck driftCheck;
    public PauseMenuScript pauseMenu;
    public LapCounter lapCounter;

    public GameObject gamePlayUI;
    public GameObject pauseMenuUI;

    public PlayerCarController playerCarController;

    public TextMeshProUGUI StyleScoreText;
    public TextMeshProUGUI Speedometer;
    public TextMeshProUGUI driftScoreText;
    public TextMeshProUGUI grazeScoreText;
    public TextMeshProUGUI lapTimeText;
    public TextMeshProUGUI bestLapTimeText;
    public TextMeshProUGUI highScoreText;
    private void Awake()
    {
        gamePlayUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (pauseMenu.gamePaused == false)
        {
            gamePlayUI.SetActive(true);
            pauseMenuUI.SetActive(false);
            StyleScoreText.text = string.Format("<mspace=1em>{0}</mspace>", driftCheck.totalScore.ToString());
            highScoreText.text = string.Format("<mspace=1em>{0}</mspace>", lapCounter.highScore.ToString());
            Speedometer.text = Mathf.Round(playerCarController.velocity.z).ToString() + " km/h";
            lapTimeText.text = FormatTime(lapCounter.lapTime);
            bestLapTimeText.text = FormatTime(lapCounter.bestLapTime);

            if (driftCheck.driftScore > 0)
            {
                driftScoreText.gameObject.SetActive(true);
                driftScoreText.text = string.Format("<mspace=1em>{0}</mspace>", driftCheck.driftScore.ToString());
            }
            else
            {
                driftScoreText.gameObject.SetActive(false);
            }
            if (driftCheck.grazeScore > 0)
            {
                grazeScoreText.gameObject.SetActive(true);
                grazeScoreText.text = string.Format("<mspace=1em>{0}</mspace>", driftCheck.grazeScore.ToString());
            }
            else
            {
                grazeScoreText.gameObject.SetActive(false);
            }
        }
        else if (pauseMenu.gamePaused == true)
        {
            gamePlayUI.SetActive(false);
            pauseMenuUI.SetActive(true);

        }

    }
    private String FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt(time * 100 % 100);

        return string.Format("<mspace=1em>{0:00}:{1:00}:{2:00}</mspace>", minutes, seconds, milliseconds);
    }
}
