using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    // List of all checkpoints in the race
    public List<Transform> checkpoints;
    private StackArray<Transform> checkpointsStack = new StackArray<Transform>();
    private Transform currentCheckpoint;
    
    [SerializeField] private float extraTime = 5f; // Extra time to add when a checkpoint is reached
    private Timer timer; // Reference to the Timer script
    
    // UI elements for game over and checkpoint notifications
    public GameObject gameOverPanel;
    public GameObject timerPanel;
    public TMP_Text gameOverText;
    public GameObject reachedCheckpointUI;
    public TMP_Text reachedCheckpointText;
    public GameObject minimapWindow;
    public GameObject pauseMenu;

    [SerializeField] private Color checkpointColour;
    
    private AudioManager audioManager;
    
    private bool isPaused = false;

    private bool isPlayed;
    private bool hasEnded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        InitializeCheckpoints();
        timer = FindFirstObjectByType<Timer>();
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    void Update()
    {
        CheckForLoss();
        
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverPanel.activeSelf)
        {
            TogglePause();
        }
    }

    private void InitializeCheckpoints()
    {
        // Pushes all checkpoints onto the stack in reverse order
        for (int i = checkpoints.Count - 1; i >= 0; i--)
        {
            checkpointsStack.Push(checkpoints[i]);
        }

        // Sets the first checkpoint and highlights it
        if (checkpointsStack.Count() > 0)
        {
            currentCheckpoint = checkpointsStack.Peek();
            HighlightCheckpoint(currentCheckpoint, checkpointColour);
        }
    }

    public void CheckpointReached(Transform checkpoint)
    {
        if (checkpoint == currentCheckpoint)
        {
            audioManager.PlaySound("Checkpoint");
            CheckpointUI(true); // Displays success message
            checkpointsStack.Pop(); // Removes checkpoint from stack
            checkpoint.gameObject.SetActive(false);
            
            // Add extra time to the timer when the checkpoint is reached
            if (timer != null)
            {
                timer.AddTime(extraTime);
            }

            // Sets the next checkpoint or ends the game if all are completed
            if (checkpointsStack.Count() > 0)
            {
                currentCheckpoint = checkpointsStack.Peek();
                HighlightCheckpoint(currentCheckpoint, checkpointColour);
            }
            else
            {
                // end game
                EndGame(true);
                audioManager.PlaySound("Win");
            }
        }
        else
        {
            CheckpointUI(false); // Displays wrong checkpoint message
        }
    }
    
    // Checks if the player loses due to the timer reaching zero
    public void CheckForLoss()
    {
        if (!hasEnded && timer.RemainingTime <= 0 && checkpointsStack.Count() > 0)
        {
            hasEnded = true; // Prevent further EndGame calls
            EndGame(false);

            if (!isPlayed)
            {
                audioManager.PlaySound("Lose");
                isPlayed = true;
            }
        }
    }

    private void EndGame(bool hasWon)
    {
        Time.timeScale = 0;  // Pause the game
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // Displays appropriate message based on win/lose condition
        gameOverText.text = hasWon ? "All Checkpoints Completed. You Win!" : "Time's Up! You Lose.";
        timerPanel.SetActive(false);
        reachedCheckpointUI.SetActive(false);
        minimapWindow.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    private void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if (!isPaused)
        {
            audioManager.PlaySound("ButtonClick");
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            isPaused = true;

            timerPanel.SetActive(false);
            reachedCheckpointUI.SetActive(false);
            minimapWindow.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        isPaused = false;

        timerPanel.SetActive(true);
        pauseMenu.SetActive(false);
        minimapWindow.SetActive(true);
    }

    // Changes the colour of a checkpoint to indicate status 
    private void HighlightCheckpoint(Transform checkpoint, Color colour)
    {
        if (checkpoint.childCount > 0)
        {
            for (int i = 0; i < checkpoint.childCount; i++)
            {
                Transform child = checkpoint.GetChild(i);
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = colour;
                }
            }
        }
    }

    // Handles UI feedback when reaching a checkpoint
    private void CheckpointUI(bool correctCheckpoint)
    {
        if (correctCheckpoint == true)
        {
            reachedCheckpointText.text = "Checkpoint Reached!";
            reachedCheckpointText.color = Color.white;
        }
        else
        {
            audioManager.PlaySound("Warning");
            reachedCheckpointText.text = "!! Wrong Checkpoint !!";
            reachedCheckpointText.color = Color.red;
        }
        
        StartCoroutine(CheckpointUI());
    }
    
    private IEnumerator CheckpointUI()
    {
        reachedCheckpointUI.SetActive(true);
        yield return new WaitForSeconds(3);
        reachedCheckpointUI.SetActive(false);
    }
}
