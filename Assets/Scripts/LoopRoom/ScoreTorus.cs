using UnityEngine;

public class ScoreTorus : MonoBehaviour
{
    public AudioClip scoreSound;
    private AudioSource audioSource;

    public int totalScore = 0;

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
        if (other.CompareTag("Sphere"))
        {
            Debug.Log("Scored!");
            if (scoreSound != null)
                audioSource.PlayOneShot(scoreSound);

            totalScore += 1;
        }
    }
}
