using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

// Manages the race, tracks all players, and updates the player's position during the race
public class RaceManager : MonoBehaviour
{
    // List of all racers in the race
    private List<PositionTracker> allRacers = new List<PositionTracker>();
    
    // List of sorted racers by position
    public List<PositionTracker> SortedRacers { get; private set; } = new List<PositionTracker>();
    
    // The player's current position in the race
    public int PlayerPosition { get; private set; }
    
    public TextMeshProUGUI positionTextInGame;
    public TextMeshProUGUI positionTextGameOver;

    void Start()
    {
        // Get all racers at start
        allRacers = FindObjectsByType<PositionTracker>(FindObjectsSortMode.None).ToList();
    }

    void Update()
    {
        // Continuously update race positions and player's position display
        UpdateRacePositions();
        SetPosition();
    }

    // Updates the positions of all racers by sorting them based on waypoints passed and distance to the next waypoint
    private void UpdateRacePositions()
    {
        SortedRacers = allRacers
            // Sort first by number of waypoints passed (descending), so racers who passed more waypoints are higher
            .OrderByDescending(r => r.WaypointsPassed)
            // Then, sort by distance to the next waypoint (ascending), so racers who are closer to the next waypoint are ranked higher
            .ThenBy(r => r.DistanceToNextWaypoint())
            .ToList();

        // Find the player's position in the sorted list
        PlayerPosition = SortedRacers.FindIndex(r => r.isPlayer) + 1;
    }

    // Converts an integer position to its corresponding ordinal suffix
    string GetOrdinal(int number)
    {
        if (number % 100 is >= 11 and <= 13) return "th";
        return (number % 10) switch
        {
            1 => "st",
            2 => "nd",
            3 => "rd",
            _ => "th"
        };
    }

    // Updates the player's position on the UI
    void SetPosition()
    {
        int pos = PlayerPosition;
        positionTextInGame.text = $"{pos}{GetOrdinal(pos)} PLACE";
        positionTextGameOver.text = $"{pos}{GetOrdinal(pos)} PLACE";
    }
}
