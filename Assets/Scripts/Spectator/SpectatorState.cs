// Abstract base class representing a generic state for a Spectator
public abstract class SpectatorState
{
    // Reference to the Spectator that owns this state
    protected Spectator spectator;

    // Constructor assigns the owning Spectator
    public SpectatorState(Spectator spectator)
    {
        this.spectator = spectator;
    }

    // Called when the state is first entered
    public abstract void EnterState();
    
    // Called every frame while this state is active 
    public abstract void UpdateState();
}
