// Low speed and handling, best for controlled, careful movement.
public class SlowRacer : AIRacer
{
    protected override void Start()
    {
        speed = 5f;
        acceleration = 6f;
        angularSpeed = 60f;
        stoppingDistance = 1f;
        
        base.Start();
    }
}
