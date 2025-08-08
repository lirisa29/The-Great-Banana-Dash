using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LapTracker : MonoBehaviour
{
    public int CurrentLap { get; private set; } = 1;
    public int TotalLaps = 3;

    // Prevents multiple lap counts from a single trigger pass
    private bool hasCrossed = false;
    
    private WaypointManager waypointManager;
    private int totalLinearWaypoints;
    
    // Tracks which waypoints have been visited this lap
    private HashSet<int> waypointsVisited = new HashSet<int>();
    
    private List<int> pathThisLap = new List<int>();
    
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject ingameUI;

    // Delegate and event to notify other scripts when lap changes
    public delegate void LapChanged(int currentLap, int totalLaps);
    public event LapChanged OnLapChanged;

    private string currentScene;
    
    private AudioManager audioManager;
    
    private bool isPaused = false;
    public GameObject pauseMenu;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        waypointManager = FindFirstObjectByType<WaypointManager>();
        audioManager = FindFirstObjectByType<AudioManager>();
        currentScene = SceneManager.GetActiveScene().name;

        if (waypointManager != null && currentScene == "Beginner_Race")
        {
            totalLinearWaypoints = waypointManager.waypoints.Size;
        }
        
        // Notify any listeners of the initial lap status
        OnLapChanged?.Invoke(CurrentLap, TotalLaps);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverScreen.activeSelf)
        {
            TogglePause();
        }
    }
    
    public void OnWaypointPassed(int waypointIndex)
    {
        if (currentScene == "Beginner_Race" && !waypointsVisited.Contains(waypointIndex))
        {
            waypointsVisited.Add(waypointIndex);
        }

        if (currentScene == "Advanced_Race" && !pathThisLap.Contains(waypointIndex))
        {
            pathThisLap.Add(waypointIndex);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish") && !hasCrossed)
        {
            hasCrossed = true;
            bool lapComplete = false;

            if (currentScene == "Beginner_Race")
            {
                lapComplete = waypointsVisited.Count >= totalLinearWaypoints;
            }
            else if (currentScene == "Advanced_Race")
            {
                lapComplete = pathThisLap.Count >= 17 && pathThisLap.Contains(0);
            }

            // Only allow lap completion if all waypoints were passed
            if (lapComplete)
            {
                audioManager.PlaySound("Checkpoint");
                CurrentLap++;

                waypointsVisited.Clear(); // Start fresh for next lap
                pathThisLap.Clear();

                OnLapChanged?.Invoke(CurrentLap, TotalLaps);
            }
            
            // If the current lap exceeds the total, trigger game over
            if (CurrentLap > TotalLaps)
            {
                GameOver();
                audioManager.PlaySound("Win");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            hasCrossed = false;
        }
    }
    
    private void GameOver()
    {
        Time.timeScale = 0f; // Pause the game
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        if (gameOverScreen != null)
        {
            ingameUI.SetActive(false); // Hide in-game UI
            gameOverScreen.SetActive(true); // Show Game Over UI
        }
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

            ingameUI.SetActive(false); // Hide in-game UI
            pauseMenu.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        isPaused = false;

        ingameUI.SetActive(true); // Hide in-game UI
        pauseMenu.SetActive(false);
    }
}
