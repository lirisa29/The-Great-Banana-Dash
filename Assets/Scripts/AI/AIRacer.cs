using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// Abstract class representing a base AI racer controlled by a NavMeshAgent
public abstract class AIRacer : MonoBehaviour
{
    protected NavMeshAgent agent;
    public float speed;
    public float acceleration;
    public float angularSpeed;
    public float stoppingDistance;
    
    protected WaypointManager waypointManager;
    private int currentWaypointIndex = 0;
    
    protected WaypointGraphBuilder waypointGraphBuilder;
    protected Transform currentWaypoint;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        // Set the parameters of the NavMeshAgent based on the values provided
        agent.speed = speed;
        agent.acceleration = acceleration;
        agent.angularSpeed = angularSpeed;
        agent.stoppingDistance = stoppingDistance;
        
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Beginner_Race")
        {
            waypointManager = FindFirstObjectByType<WaypointManager>();
            // Set the AI's next waypoint to start navigating to
            SetNextWaypoint();
        }
        else if (currentScene == "Advanced_Race"){
            waypointGraphBuilder = FindFirstObjectByType<WaypointGraphBuilder>();

            currentWaypoint = waypointGraphBuilder.vertices[0];
            SetNextWaypointGraph();
        }
    }

    public void SetNextWaypointGraph()
    {
        // Get a list of connected waypoints (neighbours) from the current waypoint
        List<Transform> neighbours = waypointGraphBuilder.Graph.GetConnectedVerticies(currentWaypoint);
        
        // Randomly select the next waypoint from the list of neighbours
        Transform nextWaypoint = ChooseNextWaypoint(neighbours);

        if (nextWaypoint != null)
        {
            // Update the current waypoint to the new target
            currentWaypoint = nextWaypoint;
            
            PositionTracker tracker = GetComponent<PositionTracker>();
            if (tracker != null && tracker.currentScene == "Advanced_Race")
            {
                tracker.nextGraphWaypointTarget = currentWaypoint;
            }
            
            agent.SetDestination(currentWaypoint.position);
        }
    }

    private Transform ChooseNextWaypoint(List<Transform> neighbours)
    {
        // Pick and return a random neighbor from the list
        return neighbours[Random.Range(0, neighbours.Count)];
    }

    public void SetNextWaypoint()
    {
        // Increment the waypoint index and loop back to the first waypoint
        currentWaypointIndex = (currentWaypointIndex + 1) % waypointManager.waypoints.Size;
        
        // Get the transform of the next waypoint from the WaypointManager
        Transform nextWaypoint = waypointManager.waypoints[currentWaypointIndex];
        
        // Tell the NavMeshAgent to set the destination to the position of the next waypoint
        agent.SetDestination(nextWaypoint.position);
    }
}
