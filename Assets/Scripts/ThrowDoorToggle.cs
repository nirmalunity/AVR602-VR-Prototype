using UnityEngine;

public class DoorToggle : MonoBehaviour
{
    public Animator doorAnimator;
    public AudioClip doorSound;
    public AudioSource audioSource;
    private bool isOpen = false;
    private bool isAnimating = false;

    private Vector3 originalScale;
    private Vector3 pressedScale = new Vector3(0.3f, 1f, 1f);
    public float scaleResetDelay = 2f; 

    public float animationDuration = 4f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void ToggleDoor()
    {
        if (isAnimating || doorAnimator == null || audioSource == null) return;

        if (isOpen)
            doorAnimator.Play("ThrowRoomClose");
        else
            doorAnimator.Play("ThrowDoorAnim");

        audioSource.PlayOneShot(doorSound);

        transform.localScale = pressedScale;

        // Reset scale after delay
        Invoke(nameof(ResetScale), scaleResetDelay);
        Invoke(nameof(ResetAnimationLock), animationDuration); 

        isOpen = !isOpen;
    }

    void ResetScale()
    {
        transform.localScale = originalScale;
    }

      void ResetAnimationLock()
    {
        isAnimating = false;
    }
}
