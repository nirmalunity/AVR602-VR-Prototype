using UnityEngine;

public class BowlingArea : MonoBehaviour
{
    public GameObject hintCanvas;
    public float displayTime = 5f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ZoneManager.Instance.SetActiveZone("Bowling");

            /*             if (hintCanvas != null)
                        {
                            hintCanvas.SetActive(true);
                            CancelInvoke(nameof(HideCanvas));
                            Invoke(nameof(HideCanvas), displayTime);
                        } */
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ZoneManager.Instance.SetActiveZone("");
        }
    }

    /*     private void HideCanvas()
        {
            if (hintCanvas != null)
                hintCanvas.SetActive(false);
        } */
}
