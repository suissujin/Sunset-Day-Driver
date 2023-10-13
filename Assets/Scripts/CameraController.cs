using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public NewController newController;
    private List<Vector3> velocityList = new List<Vector3>();
    public float angle;
    [Range(0, 1)]
    public float stiffness;
    public float followSpeed;
    public int velocityListLength;

    private void Awake()
    {
        transform.position = newController.transform.position + offset;
    }
    private void LateUpdate()
    {
        //transform.position += newController.transform.TransformDirection(newController.velocity) * Time.deltaTime;
        var targetRotation = Quaternion.Euler(0, newController.transform.eulerAngles.y, 0);
        velocityList.Add(newController.transform.TransformDirection(newController.velocity) * (Time.deltaTime * (1f - stiffness)));
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
        var targetPosition = newController.transform.position + targetRotation * offset - velocity;
        //transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.position = targetPosition;
        transform.LookAt(newController.transform.position + newController.transform.forward * 10f);
    }
}