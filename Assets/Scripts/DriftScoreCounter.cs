using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DriftScoreCounter : MonoBehaviour
{
    public DriftCheck driftCheck;
    public PauseMenuScript pauseMenu;
    public GameObject gamePlayUI;
    public GameObject pauseMenuUI;
    public PlayerCarController playerCarController;
    public TextMeshProUGUI StyleScoreText;
    public TextMeshProUGUI Speedometer;
    public TextMeshProUGUI driftScoreText;
    public TextMeshProUGUI grazeScoreText;
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
            StyleScoreText.text = "Style Points: " + driftCheck.totalScore.ToString();
            Speedometer.text = Mathf.Round(playerCarController.velocity.z).ToString() + " km/h";

            if (driftCheck.driftScore > 0)
            {
                driftScoreText.gameObject.SetActive(true);
                driftScoreText.text = "Drifting for: " + driftCheck.driftScore.ToString();
            }
            else
            {
                driftScoreText.gameObject.SetActive(false);
            }
            if (driftCheck.grazeScore > 0)
            {
                grazeScoreText.gameObject.SetActive(true);
                grazeScoreText.text = "Grazeing for: " + driftCheck.grazeScore.ToString();
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
}
