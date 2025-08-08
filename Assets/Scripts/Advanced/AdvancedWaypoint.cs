using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AdvancedWaypoint : MonoBehaviour
{
    [SerializeField]
    public int waypointIndex;
    private AudioManager audioManager;
    public GameObject warningText;
    private HashSet<GameObject> alreadyTriggered = new HashSet<GameObject>();
    
    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PositionTracker tracker = other.GetComponent<PositionTracker>();
        LapTracker lapTracker = other.GetComponent<LapTracker>();
        PlayerRespawner respawner = other.GetComponent<PlayerRespawner>();
        
        WaypointGraphBuilder graphBuilder = FindFirstObjectByType<WaypointGraphBuilder>();
        if (graphBuilder == null) return;

        if (respawner != null && respawner.isRespawning)
        {
            respawner.isRespawning = false;
            return; // Ignore trigger while respawning
        }
        
        // Only allow trigger if this is the next expected waypoint
        if (!IsValidNextWaypoint(tracker.lastVisitedWaypoint, this.transform))
        {
            audioManager.PlaySound("Warning");
            StartCoroutine(WarningUI());
            return;
        }

        if (tracker.lastVisitedWaypoint == null)
        {
            Transform firstWaypoint = graphBuilder.vertices[0];
            if (this.transform != firstWaypoint) return;
        }
        
        // Update respawn and lap info only for the player
        if (other.CompareTag("Player"))
        {
            // Assign next waypoint target
            var nextConnected = graphBuilder.Graph.GetConnectedVerticies(transform);
            if (nextConnected.Count > 0)
            {
                tracker.nextGraphWaypointTarget = ChooseNextWaypointBasedOnDirection(tracker.transform, nextConnected); // pick the next waypoint
            }

            respawner.SetLastVisitedWaypoint(this.transform);
            lapTracker.OnWaypointPassed(waypointIndex);
            tracker.OnWaypointReached();
            tracker.lastVisitedWaypoint = this.transform;
        }
        
        // Set next waypoint for AI
        if (other.CompareTag("AI"))
        {
            AIRacer racer = other.GetComponent<AIRacer>();
            if (racer != null)
            {
                racer.SetNextWaypointGraph();
                tracker.OnWaypointReached();
                tracker.lastVisitedWaypoint = this.transform;
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (alreadyTriggered.Contains(other.gameObject)) return;

        // Check same logic as OnTriggerEnter
        PositionTracker tracker = other.GetComponent<PositionTracker>();
        LapTracker lapTracker = other.GetComponent<LapTracker>();
        PlayerRespawner respawner = other.GetComponent<PlayerRespawner>();

        if (tracker == null || lapTracker == null || respawner == null) return;

        // Prevent triggering while respawning
        if (respawner.isRespawning) return;

        if (!IsValidNextWaypoint(tracker.lastVisitedWaypoint, this.transform)) return;

        // Only trigger if valid
        alreadyTriggered.Add(other.gameObject);
        OnTriggerEnter(other); // reuse the existing logic
    }
    
    private void OnTriggerExit(Collider other)
    {
        alreadyTriggered.Remove(other.gameObject);
    }

    private bool IsValidNextWaypoint(Transform lastVisited, Transform current)
    {
        if (lastVisited == null) return true; // Allow first trigger

        WaypointGraphBuilder graphBuilder = FindFirstObjectByType<WaypointGraphBuilder>();
        if (graphBuilder == null) return false;
        var connected = graphBuilder.Graph.GetConnectedVerticies(lastVisited);

        bool isValid = connected.Contains(current);
        return isValid;
    }
    
    private Transform ChooseNextWaypointBasedOnDirection(Transform player, List<Transform> options)
    {
        Vector3 forward = player.GetComponent<Rigidbody>().linearVelocity.normalized;

        Transform bestChoice = null;
        float bestDot = -1f;

        foreach (Transform option in options)
        {
            Vector3 toOption = (option.position - player.position).normalized;
            float dot = Vector3.Dot(forward, toOption);

            if (dot > bestDot)
            {
                bestDot = dot;
                bestChoice = option;
            }
        }
        
        return bestChoice;
    }
    
    private IEnumerator WarningUI()
    {
        warningText.SetActive(true);
        yield return new WaitForSeconds(3);
        warningText.SetActive(false);
    }
}
