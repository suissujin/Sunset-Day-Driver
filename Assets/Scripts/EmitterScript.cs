using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class EmitterScript : MonoBehaviour
{
    public float cityValue;

    private float _cityValueMin = 0.0f;
    private float _cityValueMax = 1.0f;
    private float timeElapsed = 0.0f;
    private void Start()
    {
        if (AudioManagerScript.instance != null)
        {
            if (AudioManagerScript.instance.isMuted)
            {
                GetComponent<StudioEventEmitter>().Stop();
            }
            else
            {
                GetComponent<StudioEventEmitter>().Play();
            }
        }
    }
    private void Update()
    {
        GetComponent<StudioEventEmitter>().SetParameter("City", cityValue);
    }
    public void ApplyCityAmbient()
    {
        if (timeElapsed < 20 && cityValue != _cityValueMax)
        {
            cityValue = Mathf.Lerp(_cityValueMin, _cityValueMax, timeElapsed / 20);
            timeElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed = 0;
            cityValue = _cityValueMax;
        }
    }
    public void SubtractCityAmbient()
    {
        if (timeElapsed < 20 && cityValue != _cityValueMin)
        {
            cityValue = Mathf.Lerp(_cityValueMax, _cityValueMin, timeElapsed / 20);
            timeElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed = 0;
            cityValue = _cityValueMin;
        }
    }
}
