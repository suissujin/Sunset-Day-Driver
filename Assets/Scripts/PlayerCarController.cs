using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering.Universal;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerCarController : MonoBehaviour
{
    public CarTuning carTuning;
    public DriftCheck driftCheck;
    public PauseMenuScript pauseMenu;
    public WaypointList waypointList;
    public LapCounter lapCounter;

    public Vector3 velocity;
    public Transform carModel;
    private Transform currentWaypoint;
    public GameObject tireMarks;
    public Collider carBody;

    public BoxCollider carCollider;
    public LayerMask collisionMask;
    public LayerMask groundMask;
    public AnimationCurve collisionCurve;
    public float collisionDistance;
    private float raylength = 1f;

    private float accelerationInput;
    private float brakeInput;
    private float frontSteerInput;
    private float rearSteerInput;
    private float backingUpInput;
    public int carType;
    public int quitCounter = 0;
    public float carSpeed;
    private int currentCarType;


    public bool isDrifting = false;
    public bool onRoad;
    public bool resetting;

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
        inputActions.CarControlls.SwitchCar1.performed += ctx => carType = 1;
        inputActions.CarControlls.SwitchCar2.performed += ctx => carType = 2;
        inputActions.CarControlls.SwitchCar3.performed += ctx => carType = 3;
        inputActions.CarControlls.SwitchCar4.performed += ctx => carType = 4;
        inputActions.CarControlls.Pause.performed += ctx => pauseMenu.Pause();
        inputActions.CarControlls.ResetCar.performed += ctx => resetting = true;
        inputActions.CarControlls.QuitGame.performed += ctx => quitCounter = 1;

        SelectCar(OnStartScript.instance.carIndex);
        pauseMenu.gamePaused = false;
    }

    private void Update()
    {
        if (pauseMenu.gamePaused == true)
        {
            Gamepad.current?.SetMotorSpeeds(0, 0);
            if (quitCounter == 1)
            {
                SceneManager.LoadScene(0);
            }
            SelectCar(carType);
        }
        else { carType = currentCarType; }
    }

    void FixedUpdate()
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
        CheckCollision();
        controller.Move(transform.TransformDirection(velocity) * Time.deltaTime);

        if (brakeInput > 0.6f || isDrifting)
        {
            tireMarks.SetActive(true);
        }
        else
        {
            tireMarks.SetActive(false);
        }

        if (transform.position.y != 1.69f)
        {
            transform.position = new Vector3(transform.position.x, 1.69f, transform.position.z);
        }

        CheckRoad();
        if (resetting)
        {
            resetting = false;
            if (!onRoad)
            {
                ResetCar();
            }
        }

    }
    void SelectCar(int carType)
    {
        Transform ChildHolder = gameObject.transform.GetChild(0).gameObject.transform;
        switch (carType)
        {
            case 1:
                ChildHolder.GetChild(0).gameObject.SetActive(true);
                ChildHolder.GetChild(1).gameObject.SetActive(false);
                ChildHolder.GetChild(2).gameObject.SetActive(false);
                ChildHolder.GetChild(3).gameObject.SetActive(false);
                carTuning = Resources.Load<CarTuning>("CarTunings/Auti 1");
                currentCarType = 1;
                Debug.Log(carTuning.carName);
                break;

            case 2:
                ChildHolder.GetChild(0).gameObject.SetActive(false);
                ChildHolder.GetChild(1).gameObject.SetActive(true);
                ChildHolder.GetChild(2).gameObject.SetActive(false);
                ChildHolder.GetChild(3).gameObject.SetActive(false);
                carTuning = Resources.Load<CarTuning>("CarTunings/Auti 2");
                currentCarType = 2;
                Debug.Log(carTuning.carName);
                break;

            case 3:
                ChildHolder.GetChild(0).gameObject.SetActive(false);
                ChildHolder.GetChild(1).gameObject.SetActive(false);
                ChildHolder.GetChild(2).gameObject.SetActive(true);
                ChildHolder.GetChild(3).gameObject.SetActive(false);
                carTuning = Resources.Load<CarTuning>("CarTunings/Auti 3");
                currentCarType = 3;
                Debug.Log(carTuning.carName);
                break;

            case 4:
                ChildHolder.GetChild(0).gameObject.SetActive(false);
                ChildHolder.GetChild(1).gameObject.SetActive(false);
                ChildHolder.GetChild(2).gameObject.SetActive(false);
                ChildHolder.GetChild(3).gameObject.SetActive(true);
                carTuning = Resources.Load<CarTuning>("CarTunings/Auti 4");
                currentCarType = 4;
                Debug.Log(carTuning.carName);
                break;

            default:
                ChildHolder.GetChild(0).gameObject.SetActive(true);
                ChildHolder.GetChild(1).gameObject.SetActive(false);
                ChildHolder.GetChild(2).gameObject.SetActive(false);
                ChildHolder.GetChild(3).gameObject.SetActive(false);
                carTuning = Resources.Load<CarTuning>("CarTunings/Auti 1");
                currentCarType = 1;
                Debug.Log(carTuning.carName);
                break;
        }
    }
    void Accelerate(float amount)
    {
        float maxSpeed = onRoad ? carTuning.maxSpeed : 20f;
        var smoothedAcceleration = carTuning.accelerationCurve.Evaluate(velocity.magnitude / maxSpeed) * amount;

        if (isDrifting)
        {
            velocity.z += smoothedAcceleration * carTuning.acceleration * 0.8f * Time.deltaTime;
        }
        else
        {
            velocity.z += smoothedAcceleration * carTuning.acceleration * Time.deltaTime;
        }
    }
    void AccelerateBack(float amount)
    {
        float maxSpeed = onRoad ? carTuning.maxSpeed : 20f;
        var smoothedAcceleration = carTuning.accelerationCurve.Evaluate(velocity.magnitude / maxSpeed) * amount;

        if (isDrifting)
        {
            velocity.z -= smoothedAcceleration * carTuning.acceleration * 0.8f * Time.deltaTime;
        }
        else
        {
            velocity.z -= smoothedAcceleration * carTuning.acceleration * Time.deltaTime;
        }
    }
    void CheckCollision()
    {
        var worldVelocity = transform.TransformDirection(velocity);
        if (Physics.BoxCast(carBody.transform.position, carCollider.size * 0.5f, worldVelocity.normalized, out var hit, carModel.localRotation, collisionDistance, collisionMask))
        {
            var dot = Math.Abs(Vector3.Dot(hit.normal, worldVelocity.normalized));
            velocity *= collisionCurve.Evaluate(dot);
            Debug.Log("Collision: " + hit.distance);
            driftCheck.crashed = true;

        }
        // else { driftCheck.crashed = false; }
    }
    void CheckRoad()
    {
        if (Physics.Raycast(transform.position, Vector3.down, raylength, groundMask))
        {
            onRoad = true;
            //Debug.Log("On Road");
            Gamepad.current?.SetMotorSpeeds(0f, 0f);
        }
        else
        {
            onRoad = false;
            //Debug.Log("Off Road");
            Gamepad.current?.SetMotorSpeeds(0.05f, 0.05f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(carBody.transform.position, carCollider.size);
        Gizmos.DrawRay(transform.position, Vector3.down * raylength);
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
        else if (velocity.z < -1)
        {
            if (isDrifting && brakeInput > 0.6f)
            {
                velocity.z += carTuning.tireGrip * Time.deltaTime;
            }
            else
            {
                velocity.z += amount * carTuning.brakeStrength * Time.deltaTime;
            }
        }
        else { velocity.z = 0; }
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
    //^^^^^^^
    //goht immer nonig!!!!

    void RearSteering(ref float steeringAmount)
    {
        var targetAngle = carTuning.maxModelAngle * rearSteerInput;
        var targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
        var driftAmount = Mathf.Max(velocity.magnitude / carTuning.maxSpeed, brakeInput);
        carModel.localRotation = Quaternion.RotateTowards(carModel.localRotation, targetRotation, carTuning.rotationChangeSpeed * Time.deltaTime * carTuning.driftCurve.Evaluate(driftAmount));
        steeringAmount = rearSteerInput * carTuning.steeringCurveRear.Evaluate(driftAmount) * carTuning.rightStickWeight;
        //Debug.Log("RearSteering: " + targetAngle);
        if (Mathf.Abs(targetAngle) > driftAmount && Mathf.Abs(carModel.localRotation.y) > 0.4 && onRoad)
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

    void ResetCar()
    {
        currentWaypoint = waypointList.GetFirstAndClosestWaypoint(transform.position);
        transform.position = currentWaypoint.position;
    }
}
