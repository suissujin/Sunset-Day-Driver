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
    public bool timeCounted = false;
    public bool scoreCounted = false;

    public float lapTime = 0;
    public float bestLapTime = 0;
    public float highScore = 0;

    private void FixedUpdate()
    {
        lapTime += Time.deltaTime;
        LapCheck();
        TimeCount();
        ScoreCount();
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
        if (timeCounted && scoreCounted)
        {
            lapCounted = false;
            timeCounted = false;
            scoreCounted = false;
        }
    }

    void TimeCount()
    {
        if (lapCounted)
        {
            if (bestLapTime == 0)
            {
                bestLapTime = lapTime;
            }
            else if (lapTime < bestLapTime)
            {
                bestLapTime = lapTime;
            }
            lapTime = 0;
            timeCounted = true;
        }
    }
    void ScoreCount()
    {
        if (lapCounted)
        {
            if (highScore == 0)
            {
                highScore = driftCheck.totalScore;
            }
            else if (driftCheck.totalScore > highScore)
            {
                highScore = driftCheck.totalScore;
            }
            driftCheck.totalScore = 0;
            scoreCounted = true;
        }
    }
}

