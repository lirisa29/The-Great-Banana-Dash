using UnityEngine;
using System.Collections;

// Handles player respawning when they go off-track or get flipped over
public class PlayerRespawner : MonoBehaviour
{
    private bool isOffTrack = false;
    private float offTrackTime = 0f;
    public float respawnDelay = 2f;
    private PositionTracker tracker;
    private Rigidbody rb;
    private Transform lastVisitedWaypoint;
    private AudioManager audioManager;
    public bool isRespawning;

    void Start()
    {
        tracker = GetComponent<PositionTracker>();
        rb = GetComponent<Rigidbody>();
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    void OnCollisionExit(Collision collision)
    {
        // If the player leaves the track, start the off-track timer
        if (collision.gameObject.CompareTag("Track"))
        {
            isOffTrack = true;
            offTrackTime = Time.time;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // If the player touches the track again, cancel off-track status
        if (collision.gameObject.CompareTag("Track"))
        {
            isOffTrack = false;
        }
    }

    void Update()
    {
        // Respawn if the player has been off the track too long
        if (isOffTrack && Time.time - offTrackTime > respawnDelay)
        {
            Respawn();
        }

        // Detect if the Kart is flipped upside down
        if (Vector3.Dot(transform.up, Vector3.up) < 0.5f)
        {
            Respawn();
        }
    }

    // Called externally to update the last valid waypoint the player visited
    public void SetLastVisitedWaypoint(Transform waypoint)
    {
        lastVisitedWaypoint = waypoint;
    }

    // Respawn the player at the last visited waypoint
    void Respawn()
    {
        isRespawning = true;

        audioManager.PlaySound("Death");

        // Stop any movement
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Move the player to the last visited waypoint
        transform.position = lastVisitedWaypoint.position;
        transform.rotation = lastVisitedWaypoint.rotation;

        isOffTrack = false;
    }
}
