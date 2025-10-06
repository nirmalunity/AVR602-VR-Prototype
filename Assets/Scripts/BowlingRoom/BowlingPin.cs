using UnityEngine;

public class PinSound : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;
    private bool hasFallen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasFallen && rb.linearVelocity.magnitude > 0.5f)
        {
            hasFallen = true;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (hasFallen && rb.IsSleeping())
        {
            hasFallen = false;
        }
    }
}
