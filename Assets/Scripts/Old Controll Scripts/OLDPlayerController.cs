using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLDPlayerController : MonoBehaviour
{
    public Rigidbody playerRB;
    public WheelColliders colliders;
    public WheelMeshes meshes;
    public WheelParticles particles;
    public float accelerationInput;
    public float steeringInput;
    public float brakeInput;

    public float motorPower;
    public float brakePower;
    private float slipAngle;
    private float speed;
    public AnimationCurve steeringCurve;

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
    }
    // void InstantiateSmoke(){
    //     particles.FRWheel= Instantiate(particles.FRWheel, transform.position, transform.rotation);I
    // }
    void FixedUpdate()
    {
        speed = playerRB.velocity.magnitude;
        CheckInput();
        ApplyMotorTorque();
        ApplyWheelRotation();
        ApplySteering();
        ApplyBrakeTorque();
    }

    void CheckInput()
    {
        accelerationInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
        slipAngle = Vector3.Angle(transform.forward, playerRB.velocity - transform.forward);

        //fixed code to brake even after going on reverse by Andrew Alex 
        float movingDirection = Vector3.Dot(transform.forward, playerRB.velocity);
        if (movingDirection < -0.5f && accelerationInput > 0)
        {
            brakeInput = Mathf.Abs(accelerationInput);
        }
        else if (movingDirection > 0.5f && accelerationInput < 0)
        {
            brakeInput = Mathf.Abs(accelerationInput);
        }
        else
        {
            brakeInput = 0;
        }
    }
    void ApplyMotorTorque()
    {
        colliders.RRWheel.motorTorque = accelerationInput * motorPower;
        colliders.RLWheel.motorTorque = accelerationInput * motorPower;
    }
    void ApplySteering()
    {
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        steeringAngle += Vector3.SignedAngle(transform.forward, playerRB.velocity + transform.forward, Vector3.up);
        steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
        colliders.FRWheel.steerAngle = steeringAngle;
        colliders.FLWheel.steerAngle = steeringAngle;
    }
    void ApplyBrakeTorque()
    {
        colliders.FRWheel.brakeTorque = brakeInput * brakePower * 0.7f;
        colliders.FLWheel.brakeTorque = brakeInput * brakePower * 0.7f;
        colliders.RRWheel.brakeTorque = brakeInput * brakePower * 0.3f;
        colliders.RLWheel.brakeTorque = brakeInput * brakePower * 0.3f;
    }
    void ApplyWheelRotation()
    {
        UpdateWheel(colliders.FRWheel, meshes.FRWheel);
        UpdateWheel(colliders.FLWheel, meshes.FLWheel);
        UpdateWheel(colliders.RRWheel, meshes.RRWheel);
        UpdateWheel(colliders.RLWheel, meshes.RLWheel);
    }
    void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
    {
        Vector3 pos;
        Quaternion rot;
        coll.GetWorldPose(out pos, out rot);
        wheelMesh.transform.position = pos;
        wheelMesh.transform.rotation = rot;
    }
}

[System.Serializable]
public class WheelColliders
{
    public WheelCollider FRWheel;
    public WheelCollider FLWheel;
    public WheelCollider RRWheel;
    public WheelCollider RLWheel;
}

[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer FRWheel;
    public MeshRenderer FLWheel;
    public MeshRenderer RRWheel;
    public MeshRenderer RLWheel;
}

public class WheelParticles
{
    public ParticleSystem FRWheel;
    public ParticleSystem FLWheel;
    public ParticleSystem RRWheel;
    public ParticleSystem RLWheel;
}
