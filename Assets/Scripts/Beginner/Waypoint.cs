using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PositionTracker tracker = other.GetComponent<PositionTracker>();
        LapTracker lapTracker = other.GetComponent<LapTracker>();
        PlayerRespawner respawner = other.GetComponent<PlayerRespawner>();
        
        // Only allow trigger if this is the next expected waypoint
        WaypointManager waypointManager = FindFirstObjectByType<WaypointManager>();
        Transform expectedWaypoint = waypointManager.waypoints[tracker.CurrentWaypointIndex];
        if (expectedWaypoint != this.transform) return; // Not the correct one, skip

        // Update respawn and lap info only for the player
        if (other.CompareTag("Player"))
        {
            respawner.SetLastVisitedWaypoint(this.transform);
            lapTracker.OnWaypointPassed(tracker.CurrentWaypointIndex);
            tracker.OnWaypointReached();
        }
        
        // Set next waypoint for AI
        if (other.CompareTag("AI"))
        {
            AIRacer racer = other.GetComponent<AIRacer>();
            if (racer != null)
            {
                racer.SetNextWaypoint();
                tracker.OnWaypointReached();
            }
        }
    }
}
