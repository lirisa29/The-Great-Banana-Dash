using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager manager = FindFirstObjectByType<CheckpointManager>();
            PlayerRespawner playerRespawner = FindFirstObjectByType<PlayerRespawner>();
            if (manager != null)
            {
                manager.CheckpointReached(transform);
                playerRespawner.SetLastVisitedWaypoint(this.transform);
            }
        }
    }
}
