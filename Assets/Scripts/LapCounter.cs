using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapCounter : MonoBehaviour
{
    public DriftCheck driftCheck;

    public bool crossedLC1 = false;
    public bool crossedLC2 = false;
    public bool crossedLL = false;
    public bool lapCounted = false;

    public float lapTime = 0;
    public float bestLapTime = 0;
    public float highScore = 0;

    private void FixedUpdate()
    {
        lapTime += Time.deltaTime;
        LapCheck();
        TimeCount();
    }
    void LapCheck()
    {
        if (crossedLC1 != true && crossedLC2 == true)
        {
            crossedLC2 = false;
        }
        if (crossedLC1 != true || crossedLC2 != true && crossedLL == true)
        {
            crossedLL = false;
        }
        if (crossedLC1 == true && crossedLC2 == true && crossedLL == true)
        {
            lapCounted = true;
            crossedLC1 = false;
            crossedLC2 = false;
            crossedLL = false;
        }
    }

    void TimeCount()
    {
        if (lapCounted)
        {
            if (lapTime < bestLapTime && driftCheck.totalScore > highScore)
            {
                bestLapTime = lapTime;
                highScore = driftCheck.totalScore;
                lapTime = 0;
                driftCheck.totalScore = 0;
                lapCounted = false;
            }
            else if (lapTime > bestLapTime && driftCheck.totalScore > highScore)
            {
                lapTime = 0;
                highScore = driftCheck.totalScore;
                driftCheck.totalScore = 0;
                lapCounted = false;
            }
            else if (lapTime < bestLapTime && driftCheck.totalScore < highScore)
            {
                bestLapTime = lapTime;
                lapTime = 0;
                driftCheck.totalScore = 0;
                lapCounted = false;
            }
            else if (lapTime > bestLapTime && driftCheck.totalScore < highScore)
            {
                lapTime = 0;
                driftCheck.totalScore = 0;
                lapCounted = false;
            }
        }
    }
}

