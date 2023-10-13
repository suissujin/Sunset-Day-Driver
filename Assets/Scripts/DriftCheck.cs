using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DriftCheck : MonoBehaviour
{
    public NewController newController;
    public Collider carBody;
    public int driftScore;
    public int graceScore;
    public int totalScore;
    public bool isGraceing = false;

    private void Update()
    {
        if (newController.velocity != Vector3.zero)
        {
            if (isGraceing == true)
            {
                graceScore++;
            }
            else
            {
                totalScore += graceScore;
                graceScore = 0;
            }
            if (newController.isDrifting == true)
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
            isGraceing = true;
            //Debug.Log("Bim Lastwage");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Lastwage"))
        {
            isGraceing = false;
            //Debug.Log("Weg vom Lastwage");
        }
    }
    private void OnCollision(Collision other)
    {
        Debug.Log("Crashing");
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Crashing Lastwage");
            newController.velocity.z = 0;
            graceScore = 0;
            driftScore = 0;
        }
    }
}
