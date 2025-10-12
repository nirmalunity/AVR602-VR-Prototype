using UnityEngine;

public class BallSoundOnCollision : MonoBehaviour
{
    public AudioClip groundHitSound;
    private AudioSource audioSource;
    public float ignoreCollisionTime = 0.2f;
    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;

        if (CompareTag("Sphere"))
        {
            Destroy(gameObject, 20f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Time.time - spawnTime < ignoreCollisionTime)
            return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            float impactForce = collision.relativeVelocity.magnitude;

            audioSource.PlayOneShot(groundHitSound);
        }
    }
}
