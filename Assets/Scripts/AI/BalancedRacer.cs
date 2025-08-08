// A well-rounded racer with moderate speed, acceleration, and handling.
public class BalancedRacer : AIRacer
{
    protected override void Start()
    {
        speed = 8f;
        acceleration = 10f;
        angularSpeed = 90f;
        stoppingDistance = 0.5f;
        
        base.Start();
    }
}
