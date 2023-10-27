using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public PlayerCarController playerCarController;
    private List<Vector3> velocityList = new List<Vector3>();
    public float angle;
    [Range(0, 1)]
    public float stiffness;
    public float followSpeed;
    public int velocityListLength;

    private void Awake()
    {
        transform.position = playerCarController.transform.position + offset;
    }
    private void LateUpdate()
    {
        //transform.position += playerCarController.transform.TransformDirection(playerCarController.velocity) * Time.deltaTime;
        var targetRotation = Quaternion.Euler(0, playerCarController.transform.eulerAngles.y, 0);
        velocityList.Add(playerCarController.transform.TransformDirection(playerCarController.velocity) * (Time.deltaTime * (1f - stiffness)));
        if (velocityList.Count > velocityListLength)
        {
            velocityList.RemoveAt(0);
        }
        var velocity = Vector3.zero;
        foreach (var vel in velocityList)
        {
            velocity += vel;
        }
        velocity /= velocityList.Count;
        var targetPosition = playerCarController.transform.position + targetRotation * offset - velocity;
        //transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.position = targetPosition;
        transform.LookAt(playerCarController.transform.position + playerCarController.transform.forward * 10f);
    }
}