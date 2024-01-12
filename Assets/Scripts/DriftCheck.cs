using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class DriftCheck : MonoBehaviour
{
    public PlayerCarController playerCarController;
    public LapCounter lapCounter;
    public EmitterScript emitterScript;

    public Collider carBody;

    public int driftScore;
    public int grazeScore;
    public int totalScore;
    public bool isGrazing = false;
    public bool crashed = false;
    public bool inCity = false;


    private void Update()
    {
        if (playerCarController.velocity != Vector3.zero)
        {
            if (isGrazing == true && crashed == false)
            {
                grazeScore++;
            }
            else
            {
                totalScore += grazeScore;
                grazeScore = 0;
            }
            if (playerCarController.isDrifting == true && crashed == false)
            {
                driftScore++;
            }
            else
            {
                totalScore += driftScore;
                driftScore = 0;
            }
        }
        if (inCity)
        {
            emitterScript.ApplyCityAmbient();
        }
        else { emitterScript.SubtractCityAmbient(); }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lastwage"))
        {
            isGrazing = true;
            //Debug.Log("Bim Lastwage");
        }
        if (other.gameObject.CompareTag("City"))
        {
            inCity = true;
        }
        if (other.gameObject.CompareTag("LC1"))
        {
            Debug.Log("LC1");
            lapCounter.crossedLC1 = true;
        }
        else if (other.gameObject.CompareTag("LC2"))
        {
            Debug.Log("LC2");
            lapCounter.crossedLC2 = true;
        }
        else if (other.gameObject.CompareTag("LapLine"))
        {
            Debug.Log("LapLine");
            lapCounter.crossedLL = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Lastwage"))
        {
            isGrazing = false;
            //Debug.Log("Weg vom Lastwage");
        }
        if (other.gameObject.CompareTag("City"))
        {
            inCity = false;
        }
        if (other.gameObject.CompareTag("LapLine"))
        {
            if (lapCounter.lapCounted == true)
            {
                lapCounter.lapCounted = false;
            }
        }
    }
}
