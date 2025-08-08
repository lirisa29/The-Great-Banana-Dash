using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    void Update()
    {
        if (remainingTime <= 0)
        {
            remainingTime = 0;
        }
        else
        {
            remainingTime -= Time.deltaTime;
        }
        
        int minutes = Mathf.FloorToInt(Mathf.Max(remainingTime, 0) / 60);
        int seconds = Mathf.FloorToInt(Mathf.Max(remainingTime, 0) % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    // Method to add extra time to the timer
    public void AddTime(float timeToAdd)
    {
        remainingTime += timeToAdd;
    }
    
    // Exposes the remaining time
    public float RemainingTime => remainingTime;
}
