using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarTuning", menuName = "ScriptableObjects/CarTuning", order = 1)]

public class CarTuning : ScriptableObject
{
    public string carName;
    public float maxModelAngle;
    public float rotationChangeSpeed;
    public float rotationSnapbackSpeed;
    public float maxSpeed;
    public float acceleration;
    public float brakeStrength;
    public float tireGrip;
    public float gripStrength;
    public float airResistance;

    [Range(0, 5)]
    public float leftStickWeight;
    [Range(0, 5)]
    public float rightStickWeight;


    public AnimationCurve accelerationCurve;
    public AnimationCurve steeringCurveFront;
    public AnimationCurve steeringCurveRear;
    public AnimationCurve gripCurve;
    public AnimationCurve driftCurve;

}
