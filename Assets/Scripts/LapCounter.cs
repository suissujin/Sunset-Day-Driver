using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapCounter : MonoBehaviour
{
    public bool crossedLC1 = false;
    public bool crossedLC2 = false;
    public bool crossedLL = false;
    public bool lapCounted = false;

    public float lapTime = 0;
    public float bestLapTime = 0;

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
        if (lapCounted && bestLapTime == 0)
        {
            bestLapTime = lapTime;
        }
        else if (lapCounted == true && lapTime < bestLapTime)
        {
            bestLapTime = lapTime;
            lapTime = 0;
            lapCounted = false;
        }
        else if (lapCounted == true && lapTime > bestLapTime)
        {
            lapTime = 0;
            lapCounted = false;
        }
    }
}

