using TMPro;
using UnityEngine;

public class GameDisplayManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    public float timeRemaining = 0f;
    private bool isTimerRunning = false;
    private bool hasTimeLimit = false;

    public ScoreTorus[] scTorusList;
    public CanvasTriggerZone canvastrigger;

    private void Start()
    {
        UpdateTimerDisplay();
        UpdateScoreDisplay();
    }

    private void Update()
    {
        if (isTimerRunning && hasTimeLimit)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                isTimerRunning = false;
                canvastrigger.ShowCanvas();
            }
            UpdateTimerDisplay();
        }

        UpdateScoreDisplay();
    }

    public void StartTimer(float minutes)
    {
        if (minutes <= 0)
        {
            hasTimeLimit = false;
            isTimerRunning = false;
            timerText.text = "No Time Limit";
        }
        else
        {
            timeRemaining = minutes * 60f;
            hasTimeLimit = true;
            isTimerRunning = true;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        if (!hasTimeLimit)
            return;

        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void UpdateScoreDisplay()
    {
        int totalScore = 0;

        foreach (var scTorus in scTorusList)
        {
            if (scTorus != null)
                totalScore += scTorus.totalScore;
        }

        scoreText.text = $"Score: {totalScore}";
    }
}
