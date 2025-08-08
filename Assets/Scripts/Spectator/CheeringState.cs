using UnityEngine;
public class CheeringState : SpectatorState
{
    // Constructor passes the Spectator reference to the base class
    public CheeringState(Spectator spectator) : base(spectator) { }

    public override void EnterState()
    {
        // Trigger transition to the "Cheering" animation state
        spectator.animator.SetTrigger("ToCheering");
        
        // Apply a random start offset to the animation so multiple spectators don't look identical
        float startOffset = Random.Range(0f, 1f);
        
        // Play the "Cheering" animation from a random point in the timeline
        spectator.animator.Play("Cheering", 0, startOffset);
    }

    public override void UpdateState()
    {
        // If enough time has passed, transition to a new random state
        if (spectator.timeInState >= spectator.stateDuration)
        {
            spectator.SwitchState(spectator.GetRandomState());
        }
    }
}
