using UnityEngine;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> waypointList; // List of waypoints set in the Unity editor
    public LinkedArray<Transform> waypoints = new LinkedArray<Transform>();

    void Awake()
    {
        // Loop through the serialized list of waypoints and add each to the LinkedArray
        foreach (Transform waypoint in waypointList)
        {
            waypoints.Insert(waypoint);
        }
    }
}
