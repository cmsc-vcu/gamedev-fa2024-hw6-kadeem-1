using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float timeLimit = 30f; // 30-second timer
    private float timeRemaining;
    public TextMeshProUGUI timerText; // Assign this in the Unity Inspector
    public float speedIncrease = 1.5f; // Speed multiplier for enemies
    private bool timerRunning = true;

    void Start()
    {
        timeRemaining = timeLimit;
    }

    void Update()
    {
        if (timerRunning)
        {
            timeRemaining -= Time.deltaTime; // Countdown
            if (timeRemaining <= 0f)
            {
                timeRemaining = timeLimit; // Reset the timer
                SpeedUpEnemies(); // Increase enemy speed
            }
            UpdateTimerDisplay(); // Update the timer display
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
        timerText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
    }
    
}
