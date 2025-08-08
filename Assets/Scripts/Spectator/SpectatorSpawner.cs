using UnityEngine;

// Responsible for spawning spectators at predefined spawn points in the scene
public class SpectatorSpawner : MonoBehaviour
{
    public GameObject spectatorPrefab;
    
    public GameObject[] spectatorSpawnPoint;

    void Start()
    {
        SpawnSpectators();
    }

    // Instantiates a spectator at each defined spawn point
    private void SpawnSpectators()
    {
        for (int i = 0; i < spectatorSpawnPoint.Length; i++)
        {
            // Create a new spectator instance
            GameObject newSpectator = Instantiate(spectatorPrefab);
            
            // Set the spectator's position and rotation to match the spawn point
            newSpectator.transform.position = spectatorSpawnPoint[i].transform.position;
            newSpectator.transform.rotation = spectatorSpawnPoint[i].transform.rotation;
        }
    }
}
