using UnityEngine;

public class ConcreteAIFactory : AIFactory
{
    public enum RacerType
    {
        Fast,
        Balanced,
        Slow
    }
    
    private RacerType _racerType;

    public ConcreteAIFactory(RacerType racerType)
    {
        _racerType = racerType;
    }

    public override AIRacer CreateAIRacer()
    {
        // Get the appropriate prefab from the AIManager
        GameObject racerPrefab = AIManager.Instance.GetRacerPrefab(_racerType);

        if (racerPrefab != null)
        {
            // Instantiate the prefab and return the AIRacer component
            GameObject racerObj = Object.Instantiate(racerPrefab);
            AIRacer racer = racerObj.GetComponent<AIRacer>();
            
            return racer;
        }
        
        return null; // Return null if the prefab couldn't be found
    }
}
