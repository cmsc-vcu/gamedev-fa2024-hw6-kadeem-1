using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float timeLimit = 30f; // 30-second timer
    private float timeRemaining;
    public TextMeshProUGUI timerText; // Assign this in the Unity Inspector for countdown display
    public TextMeshProUGUI elapsedTime; // Assign this in the Unity Inspector for elapsed time display
    public float speedIncrease = 1.5f; // Speed multiplier for enemies
    private bool timerRunning = true;
    private float totalElapsedTime = 0f; // Track total elapsed time

    void Start()
    {
        timeRemaining = timeLimit;
        UpdateTimerDisplay();
        UpdateElapsedTimeDisplay();
    }

    void Update()
    {
        if (timerRunning)
        {
            float deltaTime = Time.deltaTime;
            timeRemaining -= deltaTime; // Countdown
            totalElapsedTime += deltaTime; // Track elapsed time

            if (timeRemaining <= 0f)
            {
                timeRemaining += timeLimit; // Reset the timer while maintaining any overflow
                SpeedUpEnemies(); // Increase enemy speed
                OnTimerReset(); // Notify other systems
            }

            UpdateTimerDisplay(); // Update the countdown display
            UpdateElapsedTimeDisplay(); // Update the elapsed time display
        }
    }

    void SpeedUpEnemies()
    {
        // Find all active enemies and increase their speed
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.IncreaseSpeed(speedIncrease);
        }
    }

    void UpdateTimerDisplay()
    {
        // Display the rounded remaining time
        int seconds = (int)(timeRemaining % 60);
        timerText.text = string.Format("Next wave: {0:D2} sec", seconds);
    }

    void UpdateElapsedTimeDisplay()
    {
        // Convert total elapsed time to hours, minutes, seconds, and milliseconds
        int minutes = (int)((totalElapsedTime % 3600) / 60);
        int seconds = (int)(totalElapsedTime % 60);
        int milliseconds = (int)((totalElapsedTime * 1000) % 1000);

        // Format the elapsed time as HH:MM:SS:MS
        elapsedTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", minutes, seconds, milliseconds);
    }

    public void PauseTimer()
    {
        timerRunning = false;
    }

    public void ResumeTimer()
    {
        timerRunning = true;
    }

    void OnTimerReset()
    {
        // Trigger any additional functionality needed on reset
        Debug.Log("Timer Reset!");
        
        // Optional: Play a sound effect or trigger a visual effect here
    }
}