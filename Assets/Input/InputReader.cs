using UnityEngine;
using UnityEngine.InputSystem;
using static IA_Player;

[CreateAssetMenu(fileName = "InputReader", menuName = "Input/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public Vector3 Move => playerActions.Player.Move.ReadValue<Vector2>();
    public bool DriftTriggered { get; private set; }
    public bool DriftHeld { get; private set; }
    
    private IA_Player playerActions;

    void OnEnable()
    {
        if (playerActions == null)
        {
            playerActions = new IA_Player();
            playerActions.Player.SetCallbacks(this);
        }
        playerActions.Enable();
    }

    public void Enable()
    {
        playerActions.Enable();
    }
    
    public void OnDisable()  // Disable the input actions when this is no longer in use
    {
        if (playerActions != null)
        {
            playerActions.Disable();
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        // noop
    }

    public void OnDrift(InputAction.CallbackContext context)
    {
        if (context.started) // When drift button is pressed
        {
            DriftTriggered = true;
        }
        else if (context.performed) // While holding the drift button
        {
            DriftHeld = true;
        }
        else if (context.canceled) // When drift button is released
        {
            DriftTriggered = false;
            DriftHeld = false;
        }
    }
}
