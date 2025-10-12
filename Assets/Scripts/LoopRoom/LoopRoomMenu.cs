using UnityEngine;

public class CanvasTriggerZone : MonoBehaviour
{
    public GameObject uiCanvas;
    public GameObject ScoreCanvas;
    public GameDisplayManager displayManager;
    public LoopMenuController loopmenu;
    public bool insidecricketroom = false;

    public GameObject hintCanvas;
    public float displayTime = 5f;

    private void Start() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ZoneManager.Instance.SetActiveZone("LoopRoom");

            if (hintCanvas != null)
            {
                hintCanvas.SetActive(true);
                CancelInvoke(nameof(HideCanvas));
                Invoke(nameof(HideCanvas), displayTime);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ZoneManager.Instance.SetActiveZone("");

            insidecricketroom = false;

            if (ScoreCanvas != null)
                ScoreCanvas.SetActive(false);

            if (loopmenu.menuCanvas != null)
                loopmenu.menuCanvas.SetActive(false);
        }
    }

    public void ShowCanvas()
    {
        if (uiCanvas != null && insidecricketroom == true)
            uiCanvas.SetActive(true);
    }

    private void HideCanvas()
    {
        if (hintCanvas != null)
            hintCanvas.SetActive(false);
    }
}
