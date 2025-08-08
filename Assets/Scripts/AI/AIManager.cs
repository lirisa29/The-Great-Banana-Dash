using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    private ConcreteAIFactory racerFactory;

    public GameObject[] spawnPoints; // Spawn points for the racers
    
    public GameObject fastRacerPrefab;
    public GameObject balancedRacerPrefab;
    public GameObject slowRacerPrefab;
    
    // Singleton pattern for easy access to AIManager
    public static AIManager Instance { get; private set; }

    void Awake()
    {
        // Ensure only one instance of AIManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // Randomly choose which RacerType to use
            int randomRacerType = Random.Range(0, 3);
            ConcreteAIFactory.RacerType selectedRacerType = (ConcreteAIFactory.RacerType)randomRacerType;

            // Create the factory with the selected RacerType
            racerFactory = new ConcreteAIFactory(selectedRacerType);

            // Create the racer using the factory
            AIRacer racer = racerFactory.CreateAIRacer();
            
            // Temporarily disable the NavMeshAgent to prevent it from affecting the position
            NavMeshAgent navAgent = racer.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                navAgent.enabled = false;
            }
            
            // Set the racer position at a spawn point
            racer.transform.position = spawnPoints[i].transform.position;
            
            // Re-enable the NavMeshAgent after setting the position
            if (navAgent != null)
            {
                navAgent.enabled = true;
            }
        }
    }
    
    // This method will provide prefabs to ConcreteAIFactory based on RacerType
    public GameObject GetRacerPrefab(ConcreteAIFactory.RacerType racerType)
    {
        switch (racerType)
        {
            case ConcreteAIFactory.RacerType.Fast:
                return fastRacerPrefab;
            case ConcreteAIFactory.RacerType.Balanced:
                return balancedRacerPrefab;
            case ConcreteAIFactory.RacerType.Slow:
                return slowRacerPrefab;
            default:
                return null;
        }
    }
}
