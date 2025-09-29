using UnityEngine;

public class LoopDoorToggle : MonoBehaviour
{
    public Animator LeftdoorAnimator;
     public Animator RightdoorAnimator;
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
        if (isAnimating || LeftdoorAnimator == null || RightdoorAnimator == null || audioSource == null) return;

        if (isOpen){
            LeftdoorAnimator.Play("LoopRoomClose");
            RightdoorAnimator.Play("RightDoorcloseLoopRoom");
    }
        else{
            LeftdoorAnimator.Play("LoopRoomDoor");
            RightdoorAnimator.Play("RightDoorLoopRoom");
        }

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
