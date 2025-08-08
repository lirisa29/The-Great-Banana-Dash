using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionTracker : MonoBehaviour
{
    private WaypointManager waypointManager;
    
    // Index of the next waypoint the object is expected to reach
    public int CurrentWaypointIndex { get; private set; } = 0;
    
    // Total number of waypoints the object has passed (used for ranking)
    public int WaypointsPassed { get; private set; } = 0;
    
    // Flag to distinguish player from AI
    public bool isPlayer = false;
    
    [HideInInspector]
    public string currentScene;
    
    [HideInInspector]
    public Transform nextGraphWaypointTarget;
    [HideInInspector] 
    public Transform lastVisitedWaypoint;
    
    void Start()
    {
        waypointManager = FindFirstObjectByType<WaypointManager>();
        currentScene = SceneManager.GetActiveScene().name;
        
        if (currentScene == "Advanced_Race")
        {
            var graphBuilder = FindFirstObjectByType<WaypointGraphBuilder>();
            if (graphBuilder != null && graphBuilder.vertices.Count > 0)
            {
                nextGraphWaypointTarget = graphBuilder.vertices[0];
            }
        }
    }

    // Called when the object reaches a waypoint
    public void OnWaypointReached()
    {
        if (currentScene == "Beginner_Race")
        {
            // Move to the next waypoint in a circular (looping) fashion
            CurrentWaypointIndex = (CurrentWaypointIndex + 1) % waypointManager.waypoints.Size;
        }

        WaypointsPassed++;
    }

    // Calculates the distance from the object's current position to its next waypoint
    public float DistanceToNextWaypoint()
    {
        if (currentScene == "Beginner_Race")
        {
            Transform nextWaypoint = waypointManager.waypoints[CurrentWaypointIndex];
            return Vector3.Distance(transform.position, nextWaypoint.position);
        }
        else if (currentScene == "Advanced_Race")
        {
            return Vector3.Distance(transform.position, nextGraphWaypointTarget.position);
        }
        
        return float.MaxValue;
    }
}
