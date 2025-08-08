using UnityEngine;

public class KartController : MonoBehaviour
{
    [Header("Axle Info")] 
    [SerializeField] public Transform frontLeftTire;
    [SerializeField] public Transform frontRightTire;
    [SerializeField] public Transform backLeftTire;
    [SerializeField] public Transform backRightTire;

    [Header("Motor Attributes")] 
    [SerializeField] private float maxSpeed;
    [SerializeField] private float boostSpeed;
    private float realSpeed; // Actual speed
    private float currentSpeed = 0; // Applied speed
    
    [Header("Steer Attributes")]
    private float steerDirection;

    [Header("Drifting Attributes")]
    [SerializeField] private float outwardsDriftForce = 5000;
    private float driftTime;
    private bool driftLeft = false;
    private bool driftRight = false;
    
    [Header("Refs")]
    [SerializeField] private InputReader input;
    Rigidbody rb;

    [HideInInspector] 
    public float boostTime = 0;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input.Enable();
    }

    void FixedUpdate()
    {
        HandleMotor(); // Controls movement
        HandleSteering(); // Controls turning
        TireSteer(); // Adjusts tire rotation visually
        HandleDrifting(); // Controls drifting mechanics
        HandleBoost(); // Applies speed boost if available
    }

    private void HandleMotor()
    {
        // Gets the local Z velocity of the kart to determine actual speed without applied speed
        realSpeed = transform.InverseTransformDirection(rb.linearVelocity).z;
        
        if (input.Move.y > 0) // Forward movement
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * 0.5f);
        }
        else if (input.Move.y < 0) // Reverse movement
        {
            currentSpeed = Mathf.Lerp(currentSpeed, -maxSpeed / 1.75f, 1f * Time.deltaTime);
        }
        else // No input, gradually slow down
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 1.5f);
        }
        
        // Applies calculated velocity while maintaining vertical velocity
        Vector3 vel = transform.forward * currentSpeed;
        vel.y = rb.linearVelocity.y;
        rb.linearVelocity = vel;
    }

    private void HandleSteering()
    {
        steerDirection = input.Move.x;
        
        // Adjusts steering behaviour when drifting
        if (driftLeft && !driftRight)
        {
            steerDirection = input.Move.x < 0 ? -1.5f : -0.5f;
            transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, Quaternion.Euler(0, -20f, 0), 8f * Time.deltaTime);
            rb.AddForce(transform.right * outwardsDriftForce * Time.deltaTime, ForceMode.Acceleration);
        }
        else if (driftRight && !driftLeft)
        {
            steerDirection = input.Move.x > 0 ? 1.5f : 0.5f;
            transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, Quaternion.Euler(0, 20f, 0), 8f * Time.deltaTime);
            rb.AddForce(transform.right * -outwardsDriftForce * Time.deltaTime, ForceMode.Acceleration);
        }
        else // No drift, return to normal steering
        {
            transform.GetChild(0).localRotation = Quaternion.Lerp(transform.GetChild(0).localRotation, Quaternion.Euler(0, 0f, 0), 8f * Time.deltaTime);
        }
        
        //  At lower speeds, the kart turns more, while at high speeds, the turn sensitivity is reduced to prevent excessive drifting
        float steerAmount = (realSpeed > 30) ? (realSpeed / 3) * steerDirection : (realSpeed / 1.4f) * steerDirection;
        Vector3 steerDirVect = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + steerAmount, transform.eulerAngles.z);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, steerDirVect, 10 * Time.deltaTime);
    }
    
    private void TireSteer()
    {
        float targetAngle = 180f;
        if (input.Move.x < 0) // Steer left
        {
            targetAngle = 155f;
        }
        else if (input.Move.x > 0) // Steer right
        {
            targetAngle = 205f;
        }

        // Rotates front tires for visual effect when steering
        frontLeftTire.localEulerAngles = Vector3.Lerp(frontLeftTire.localEulerAngles, new Vector3(0, targetAngle, 0), 5 * Time.deltaTime);
        frontRightTire.localEulerAngles = Vector3.Lerp(frontRightTire.localEulerAngles, new Vector3(0, targetAngle, 0), 5 * Time.deltaTime);
        
        // Spins tires to simulate motion
        float spinSpeed = (currentSpeed > 30) ? currentSpeed : realSpeed;
        frontLeftTire.GetChild(0).Rotate(-90 * Time.deltaTime * spinSpeed * 0.5f, 0, 0);
        frontRightTire.GetChild(0).Rotate(-90 * Time.deltaTime * spinSpeed * 0.5f, 0, 0);
        backLeftTire.Rotate(90 * Time.deltaTime * spinSpeed * 0.5f, 0, 0);
        backRightTire.Rotate(90 * Time.deltaTime * spinSpeed * 0.5f, 0, 0);
    }
    
    private void HandleDrifting()
    {
        if (input.DriftTriggered)
        {
            if (steerDirection > 0)
            {
                driftRight = true;
                driftLeft = false;
            }
            else if (steerDirection < 0)
            {
                driftRight = false;
                driftLeft = true;
            }
        }

        if (input.DriftHeld && currentSpeed > 15 && Mathf.Abs(input.Move.x) > 0)
        {
           driftTime += Time.deltaTime; // Tracks drift duration
        }

        if (!input.DriftTriggered || realSpeed < 10)
        {
            driftLeft = false;
            driftRight = false;
            
            // Applies a speed boost based on drift duration
            if (driftTime > 1.5 && driftTime < 4)
            {
                boostTime = 0.75f;
            }
            if (driftTime >= 4 && driftTime < 7)
            {
                boostTime = 1.5f;
            }
            if (driftTime >= 7)
            {
                boostTime = 2.5f;
            }

            // Resets drift timer
            driftTime = 0;
        }
    }
    
    private void HandleBoost()
    {
        boostTime -= Time.deltaTime; // Decreases boost duration
        if(boostTime > 0)
        {
            maxSpeed = boostSpeed; // Increases max speed temporarily
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, 1 * Time.deltaTime);
        }
        else
        {
            maxSpeed = boostSpeed - 20; // Reduces back to normal speed
        }
    }
}
