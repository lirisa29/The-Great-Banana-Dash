// High speed and quick turns, aggressive racer.
public class FastRacer : AIRacer
{
    protected override void Start()
    {
        speed = 12f;
        acceleration = 15f;
        angularSpeed = 120f;
        stoppingDistance = 0.2f;
        
        base.Start();
    }
}
