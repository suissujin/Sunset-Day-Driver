using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLDTestController : MonoBehaviour
{
    public Rigidbody frontRB;
    public Rigidbody rearRB;
    public float accelerationInput;
    public float steeringInputFront;
    public float steeringInputRear;
    public float brakeInput;

    public float motorPower;
    public float brakePower;

    private float speed;
    private float slipAngleFront;
    private float slipAngleRear;

    public AnimationCurve steeringCurve;

    void Start()
    {
    }
    void Update()
    {
        CheckInput();
        ApplyMotorTorque();
        ApplyBrakeTorque();
        FrontSteering();
        RearSteering();
    }
    void CheckInput()
    {
        accelerationInput = Input.GetAxis("Vertical");
        steeringInputFront = Input.GetAxis("Horizontal");
        slipAngleFront = Vector3.Angle(transform.forward, frontRB.velocity - transform.forward);
        slipAngleRear = Vector3.Angle(transform.forward, rearRB.velocity - transform.forward);
        //steeringInputRear = Input.GetAxis("Horizontal");
    }
    void FrontSteering()
    {
        float steeringAngleFront = steeringInputFront * steeringCurve.Evaluate(speed);
        steeringAngleFront += Vector3.SignedAngle(transform.forward, frontRB.velocity + transform.forward, Vector3.forward);
        steeringAngleFront = Mathf.Clamp(steeringAngleFront, -90f, 90f);
        frontRB.transform.localRotation = Quaternion.Euler(0f, steeringAngleFront, 0f);
    }
    void RearSteering()
    {
        float steeringAngleRear = steeringInputRear * steeringCurve.Evaluate(speed);
        steeringAngleRear += Vector3.SignedAngle(transform.forward, rearRB.velocity + transform.forward, Vector3.forward);
        steeringAngleRear = Mathf.Clamp(steeringAngleRear, -90f, 90f);
        rearRB.transform.localRotation = Quaternion.Euler(0f, steeringAngleRear, 0f);
    }
    void ApplyMotorTorque()
    {
        frontRB.AddForce(frontRB.transform.forward * accelerationInput * motorPower);
        rearRB.AddForce(rearRB.transform.forward * accelerationInput * motorPower);
    }
    void ApplyBrakeTorque()
    {
        frontRB.AddForce(frontRB.transform.forward * brakeInput * brakePower * 0.7f);
        rearRB.AddForce(rearRB.transform.forward * brakeInput * brakePower * 0.3f);
    }

}

