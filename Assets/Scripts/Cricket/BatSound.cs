using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;


public class BallSoundOnHit : MonoBehaviour
{
    public AudioClip hitSound;
    private AudioSource audioSource;

    [SerializeField] private HapticImpulsePlayer hapticPlayer;//right controller for now.

private float amplitude = 0.5f;      // 0 to 1
 private float duration = 0.2f;   

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))  
        {
            audioSource.PlayOneShot(hitSound);
            hapticPlayer.SendHapticImpulse(amplitude, duration);
        }
    }
}
