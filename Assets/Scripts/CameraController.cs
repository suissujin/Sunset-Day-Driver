using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public PlayerCarController playerCarController;
    public PauseMenuScript pauseMenu;
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
    private void Update()
    {
        if (pauseMenu.gamePaused == true)
        {
            switch (playerCarController.carType)
            {
                case 1:
                    offset.y = 2.2f;
                    break;
                case 2:
                    offset.y = 4.2f;
                    break;
                case 3:
                    offset.y = 2.2f;
                    break;
                case 4:
                    angle = 2.5f;
                    break;
                default:
                    break;
            }
        }
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

        var targetFoV = 60 + playerCarController.velocity.magnitude * 0.5f;
        Camera.main.fieldOfView = Mathf.Clamp(targetFoV, 60, 100);
        //Debug.Log(Camera.main.fieldOfView);
    }
}