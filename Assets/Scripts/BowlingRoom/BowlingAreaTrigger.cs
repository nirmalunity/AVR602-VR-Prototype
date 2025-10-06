using System.Collections;
using UnityEngine;

public class BowlingAreaTrigger : MonoBehaviour
{
    private PinManager pinManager;

    public GameObject greenLight;
    public GameObject redLight;

    private Collider col;

    void Start()
    {
        SetGreenLight(true);
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BowlingBall"))
        {
            pinManager = FindFirstObjectByType<PinManager>();
            if (pinManager != null)
            {
                SetGreenLight(false);

                StartCoroutine(DelayedThrow(pinManager));
            }
            Destroy(other.gameObject, 10);
        }
    }

    private IEnumerator DelayedThrow(PinManager pm)
    {
        yield return new WaitForSeconds(3f);
        col.isTrigger = !col.isTrigger;
        yield return new WaitForSeconds(15f);
        pm.OnThrow();

        SetGreenLight(true);
        col.isTrigger = !col.isTrigger;
    }

    public void SetGreenLight(bool state)
    {
        if (greenLight != null)
            greenLight.SetActive(state);
        if (redLight != null)
            redLight.SetActive(!state);
    }
}
