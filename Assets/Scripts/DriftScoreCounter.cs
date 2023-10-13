using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DriftScoreCounter : MonoBehaviour
{
    public DriftCheck driftCheck;
    public NewController newController;
    public TextMeshProUGUI StyleScoreText;
    public TextMeshProUGUI Speedometer;
    public TextMeshProUGUI driftScoreText;
    public TextMeshProUGUI graceScoreText;

    private void Update()
    {
        StyleScoreText.text = "Style Points: " + driftCheck.totalScore.ToString();
        Speedometer.text = Mathf.Round(newController.velocity.z).ToString() + " km/h";

        if (driftCheck.driftScore > 0)
        {
            driftScoreText.gameObject.SetActive(true);
            driftScoreText.text = "Drifting for: " + driftCheck.driftScore.ToString();
        }
        else
        {
            driftScoreText.gameObject.SetActive(false);
        }
        if (driftCheck.graceScore > 0)
        {
            graceScoreText.gameObject.SetActive(true);
            graceScoreText.text = "Graceing for: " + driftCheck.graceScore.ToString();
        }
        else
        {
            graceScoreText.gameObject.SetActive(false);
        }

    }
}
