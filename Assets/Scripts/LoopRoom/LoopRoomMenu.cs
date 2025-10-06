using UnityEngine;

public class CanvasTriggerZone : MonoBehaviour
{
    public GameObject uiCanvas;
    public GameObject ScoreCanvas;
    public GameDisplayManager displayManager;
    public bool insideroom = false;

    private void Start()
    {
        if (uiCanvas != null)
            uiCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideroom = true;
            if (uiCanvas != null)
                uiCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideroom = false;
            if (uiCanvas != null)
                uiCanvas.SetActive(false);

            if (ScoreCanvas != null)
                ScoreCanvas.SetActive(false);
        }
    }

    public void ShowCanvas()
    {
        if (uiCanvas != null && insideroom == true)
            uiCanvas.SetActive(true);
    }

    public void TwoMinutes() => StartTimerAndHide(2);

    public void fiveMinutes() => StartTimerAndHide(5);

    public void tenMinutes() => StartTimerAndHide(10);

    public void noTimeLimit() => StartTimerAndHide(0);

    private void StartTimerAndHide(float minutes)
    {
        if (displayManager != null)
            displayManager.StartTimer(minutes);

        if (uiCanvas != null)
            uiCanvas.SetActive(false);

        if (ScoreCanvas != null)
            ScoreCanvas.SetActive(true);
    }
}
