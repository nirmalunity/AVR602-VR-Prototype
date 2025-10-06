using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpeakerMusic : MonoBehaviour
{
    public AudioSource audioSource;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
    }

    void OnEnable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnInteract);
            interactable.activated.AddListener(OnActivated); 
        }
    }

    void OnDisable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnInteract);
            interactable.activated.RemoveListener(OnActivated);
        }
    }

    private void OnInteract(SelectEnterEventArgs args)
    {
        ToggleMusic();
    }

    private void OnActivated(ActivateEventArgs args)
    {
        ToggleMusic();
    }

    private void ToggleMusic()
    {
        if (audioSource != null)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
            else
                audioSource.Play();
        }
    }
}
