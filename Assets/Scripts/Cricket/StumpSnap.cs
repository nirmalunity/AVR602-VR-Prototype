using UnityEngine;

using System.Collections;

public class AutoSnapSocket : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;    
    public float snapDuration = 2f;    
     public float snapDelay = 3f; 
   void Start()
    {
        
        socket.enabled = false;
    }


private void OnTriggerStay(Collider other)
{
  //  if (other.CompareTag("Stump") && !socket.enabled)
  if ( !socket.enabled)
    {
     
        if (Vector3.Angle(other.transform.up, Vector3.up) > 60f) {
            StartCoroutine(DelayedSnap());
        }
    }
}

 private IEnumerator DelayedSnap()
    {
        // Wait before enabling the snap
        yield return new WaitForSeconds(snapDelay);

        // Enable socket for a short duration
        socket.enabled = true;
        yield return new WaitForSeconds(snapDuration);
        socket.enabled = false;
    }

}

