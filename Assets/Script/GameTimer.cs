using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 180f;
    public TextMeshProUGUI timerText;

    private bool timerRunning = true;

    void Update()
    {
        if (!timerRunning)
            return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining < 0)
                timeRemaining = 0;

            UpdateTimerDisplay();
        }
        else
        {
            timerRunning = false;
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = "Time: " +
                         minutes.ToString("00") +
                         ":" +
                         seconds.ToString("00");

        // Color changes based on remaining time
        if (timeRemaining > 60f)
        {
            // More than 1 minute = White
            timerText.color = Color.white;
        }
        else if (timeRemaining > 30f)
        {
            // 1 minute to 31 seconds = Yellow
            timerText.color = Color.yellow;
        }
        else
        {
            // 30 seconds or less = Red
            timerText.color = Color.red;
        }
    }
}