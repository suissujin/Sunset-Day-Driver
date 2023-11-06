using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DriftScoreCounter : MonoBehaviour
{
    public DriftCheck driftCheck;
    public PlayerCarController playerCarController;
    public TextMeshProUGUI StyleScoreText;
    public TextMeshProUGUI Speedometer;
    public TextMeshProUGUI driftScoreText;
    public TextMeshProUGUI grazeScoreText;

    private void Update()
    {
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
}
