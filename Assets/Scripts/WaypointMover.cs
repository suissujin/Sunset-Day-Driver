using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private WaypointList waypointList;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float distanceThreshold = 0.1f;

    private Transform currentWaypoint;
    private Quaternion targetRotation;
    private Vector3 directionToWaypoint;
    private Vector3 startPosition;
    private bool isMoving = false;

    private void Start()
    {
        currentWaypoint = waypointList.GetFirstAndClosestWaypoint(transform.position);
        transform.position = currentWaypoint.position;
        startPosition = transform.position;
        currentWaypoint = waypointList.GetNextWaypoint(currentWaypoint);
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
            {
                currentWaypoint = waypointList.GetNextWaypoint(currentWaypoint);
            }
            RotateTowardsWaypoint();
        }
    }

    private void RotateTowardsWaypoint()
    {
        directionToWaypoint = (currentWaypoint.position - transform.position).normalized;
        targetRotation = Quaternion.LookRotation(directionToWaypoint);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isMoving = true;
            //Debug.Log("Player Entered");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isMoving = false;
            transform.position = startPosition;
            //Debug.Log("Player Exited");
        }
    }
}
