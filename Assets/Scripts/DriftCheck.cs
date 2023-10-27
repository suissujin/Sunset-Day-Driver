using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DriftCheck : MonoBehaviour
{
    public NewController newController;
    public Collider carBody;
    public int driftScore;
    public int grazeScore;
    public int totalScore;
    public bool isGrazing = false;
    public bool crashed = false;

    private void Update()
    {
        if (newController.velocity != Vector3.zero)
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
            if (newController.isDrifting == true && crashed == false)
            {
                driftScore++;
            }
            else
            {
                totalScore += driftScore;
                driftScore = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lastwage"))
        {
            isGrazing = true;
            //Debug.Log("Bim Lastwage");
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Crashing Lastwage");
            crashed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Lastwage"))
        {
            isGrazing = false;
            //Debug.Log("Weg vom Lastwage");
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            crashed = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Crashing Lastwage");
            crashed = true;
            newController.velocity = Vector3.zero;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            crashed = false;
        }
    }
}
