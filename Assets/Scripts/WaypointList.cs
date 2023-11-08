using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointList : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, 0.1f);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }

    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        if (currentWaypoint == null)
        {
            return transform.GetChild(0);
        }
        if (currentWaypoint.GetSiblingIndex() > 0)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() - 1);
        }
        else
        {
            return transform.GetChild(transform.childCount - 1);
        }

    }

    /// <summary>
    /// Returns the first waypoint in the list and the closest to the given position.
    /// </summary>
    /// <param name="fromPosition"></param>
    /// <returns>The closest waypoint to the given position.</returns>
    public Transform GetFirstAndClosestWaypoint(Vector3 fromPosition)
    {

        Transform closestWaypoint = null;

        foreach (Transform waypoint in transform)
        {

            if (closestWaypoint == null)
            {
                closestWaypoint = waypoint;
                continue;
            }

            if (Vector3.Distance(fromPosition, waypoint.position) < Vector3.Distance(fromPosition, closestWaypoint.position))
            {
                closestWaypoint = waypoint;
            }

        }

        return closestWaypoint;

    }

}
