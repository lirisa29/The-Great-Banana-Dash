using UnityEngine;
using TMPro;

public class Lap : MonoBehaviour
{
    public LapTracker lapTracker;
    public TextMeshProUGUI lapText;

    void Start()
    {
        UpdateLapText(lapTracker.CurrentLap, lapTracker.TotalLaps);
        // Subscribe to the OnLapChanged event to update the display when lap changes
        lapTracker.OnLapChanged += UpdateLapText;
    }

    // Method to update the lap text UI with the current lap status
    void UpdateLapText(int currentLap, int totalLaps)
    {
        lapText.text = $"{currentLap} / {totalLaps}";
    }
}
