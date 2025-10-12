using UnityEngine;
using UnityEngine.Events;

public class PlayAreaTrigger : MonoBehaviour
{
    public UnityEvent onPlayerEnter;
    public UnityEvent onPlayerExit;

    public GameObject hintCanvas;
    public float displayTime = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ZoneManager.Instance.SetActiveZone("Cricket");

            onPlayerEnter.Invoke();

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
            onPlayerExit.Invoke();
        }
    }

    private void HideCanvas()
    {
        if (hintCanvas != null)
            hintCanvas.SetActive(false);
    }
}
