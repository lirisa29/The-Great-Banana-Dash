using UnityEngine;

public class Spectator : MonoBehaviour
{
    public Animator animator;
    private SpectatorState currentState;
    public float timeInState = 0f;
    public float stateDuration = 0f;

    void Start()
    {
        // Initialize the spectator with a random state at the start
        SwitchState(GetRandomState());
    }

    void Update()
    {
        // Increment the time spent in the current state
        timeInState += Time.deltaTime;
        
        currentState.UpdateState();
    }

    // Switches to a new spectator state and resets the state timer
    public void SwitchState(SpectatorState newState)
    {
        currentState = newState;
        
        currentState.EnterState();
        
        // Set a new random duration for this state
        stateDuration = Random.Range(5f, 10f);
        timeInState = 0f;
    }

    // Returns a randomly selected state for the spectator to enter
    public SpectatorState GetRandomState()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                return new IdleState(this);
            case 1:
                return new CheeringState(this);
            case 2:
                return new DancingState(this);
            default:
                return new IdleState(this);
        }
    }
}
