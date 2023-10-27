using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewController : MonoBehaviour
{
    public CarTuning carTuning;
    public DriftCheck driftCheck;
    public Vector3 velocity;
    public Transform carModel;
    public GameObject tireMarks;
    public Collider carBody;

    private float accelerationInput;
    private float brakeInput;
    private float frontSteerInput;
    private float rearSteerInput;
    private float backingUpInput;

    public bool isDrifting = false;

    private CarController inputActions;
    [SerializeField]
    private CharacterController controller;

    private void Awake()
    {
        inputActions = new CarController();
        inputActions.Enable();
        inputActions.CarControlls.accelerate.performed += ctx => accelerationInput = ctx.ReadValue<float>();
        inputActions.CarControlls.accelerate.canceled += ctx => accelerationInput = 0;
        inputActions.CarControlls.brake.performed += ctx => brakeInput = ctx.ReadValue<float>();
        inputActions.CarControlls.brake.canceled += ctx => brakeInput = 0;
        inputActions.CarControlls.frontSteering.performed += ctx => frontSteerInput = ctx.ReadValue<float>();
        inputActions.CarControlls.frontSteering.canceled += ctx => frontSteerInput = 0;
        inputActions.CarControlls.rearSteering.performed += ctx => rearSteerInput = ctx.ReadValue<float>();
        inputActions.CarControlls.rearSteering.canceled += ctx => rearSteerInput = 0;
        inputActions.CarControlls.backingUp.performed += ctx => backingUpInput = ctx.ReadValue<float>();
        inputActions.CarControlls.backingUp.canceled += ctx => backingUpInput = 0;
    }

    void Update()
    {
        //Debug.Log("Brakeing for: " + brakeInput);
        if (brakeInput > 0)
        {
            Brake(brakeInput);
        }
        else if (accelerationInput > 0)
        {
            Accelerate(accelerationInput);
        }
        else { AccelerateBack(backingUpInput); }

        var steeringAmount = 0f;

        AirResistance();
        RearSteering(ref steeringAmount);
        FrontSteering(ref steeringAmount);
        Turn(steeringAmount);

        velocity = Vector3.ClampMagnitude(velocity, carTuning.maxSpeed);
        controller.Move(transform.TransformDirection(velocity) * Time.deltaTime);

        if (brakeInput > 0.6f || isDrifting)
        {
            tireMarks.SetActive(true);
        }
        else
        {
            tireMarks.SetActive(false);
        }

    }
    void Accelerate(float amount)
    {
        var smoothedAcceleration = carTuning.accelerationCurve.Evaluate(velocity.magnitude / carTuning.maxSpeed) * amount;
        velocity.z += smoothedAcceleration * carTuning.acceleration * Time.deltaTime;
    }
    void AccelerateBack(float amount)
    {
        var smoothedAcceleration = carTuning.accelerationCurve.Evaluate(velocity.magnitude / carTuning.maxSpeed) * amount;
        velocity.z -= smoothedAcceleration * carTuning.acceleration * Time.deltaTime;
    }

    void AirResistance()
    {
        if (velocity.magnitude > 0)
        {
            velocity.z = Mathf.Lerp(velocity.z, 0, carTuning.airResistance * Time.deltaTime);
        }
    }

    void Brake(float amount)
    {
        if (velocity.z > 0)
        {
            if (isDrifting && brakeInput > 0.6f)
            {
                velocity.z -= carTuning.tireGrip * Time.deltaTime;
            }
            else
            {
                velocity.z -= amount * carTuning.brakeStrength * Time.deltaTime;
            }
        }
        else
        {
            velocity.z = 0;
        }
    }
    void FrontSteering(ref float steeringAmount)
    {
        var steeringAbsolute = Mathf.Abs(steeringAmount);
        var maxRearTurning = carTuning.steeringCurveRear.Evaluate(1) * carTuning.rightStickWeight;
        var steeringNormalized = steeringAbsolute / maxRearTurning;
        steeringAmount += frontSteerInput * carTuning.steeringCurveFront.Evaluate(velocity.magnitude / carTuning.maxSpeed) * carTuning.leftStickWeight * carTuning.gripCurve.Evaluate(carTuning.tireGrip);
        //Debug.Log("FrontSteering: " + frontSteerInput * steeringCurveFront.Evaluate(velocity.magnitude / maxSpeed));    
    }
    //mache dass bim brämse grip 100% isch
    //rotationAngle vo gripp abhänig mache maybe?
    void RearSteering(ref float steeringAmount)
    {
        var targetAngle = carTuning.maxModelAngle * rearSteerInput;
        var targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
        var driftAmount = Mathf.Max(velocity.magnitude / carTuning.maxSpeed, brakeInput);
        carModel.localRotation = Quaternion.RotateTowards(carModel.localRotation, targetRotation, carTuning.rotationChangeSpeed * Time.deltaTime * carTuning.driftCurve.Evaluate(driftAmount));
        steeringAmount = rearSteerInput * carTuning.steeringCurveRear.Evaluate(driftAmount) * carTuning.rightStickWeight;
        //Debug.Log("RearSteering: " + targetAngle);
        if (Mathf.Abs(targetAngle) > driftAmount && Mathf.Abs(carModel.localRotation.y) > 0.4)
        {
            isDrifting = true;
        }
        else
        {
            isDrifting = false;
        }

    }
    void Turn(float steeringAmount)
    {
        transform.Rotate(0f, steeringAmount, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
