using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 300f;
    public TextMeshProUGUI timerText;

    private bool timerRunning = true;

    void Start()
    {
        UpdateTimerDisplay();
    }

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
            TimerFinished();
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

        // White: more than 1 minute
        if (timeRemaining > 60f)
        {
            timerText.color = Color.white;
        }
        // Yellow: 1 minute to 31 seconds
        else if (timeRemaining > 30f)
        {
            timerText.color = Color.yellow;
        }
        // Red: 30 seconds or less
        else
        {
            timerText.color = Color.red;
        }
    }

    void TimerFinished()
    {
        timerText.text = "Time's Up!";
        timerText.color = Color.red;
    }
}