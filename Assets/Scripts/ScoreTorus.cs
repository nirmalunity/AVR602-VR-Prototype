using UnityEngine;

public class ScoreTorus : MonoBehaviour
{
    public AudioClip scoreSound;        
    private AudioSource audioSource;

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Scored!");
                  if (scoreSound != null)
                audioSource.PlayOneShot(scoreSound);
        }
    }
}
