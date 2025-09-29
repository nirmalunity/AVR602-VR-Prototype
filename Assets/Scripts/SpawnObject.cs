using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnOnGrab : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject objectPrefab; // The prefab to spawn
    public float spawnDistanceThreshold = 0.5f; // Distance from original position to trigger spawn
    public bool spawnOnGrab = true; // Spawn immediately when grabbed
    public bool spawnOnDistance = true; // Spawn when moved away from original position

    [Header("Audio")]
    public AudioClip spawnSound;
    public AudioSource audioSource;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool hasSpawned = false;
    private bool isGrabbed = false;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        // Store the original position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // Get the XR Grab Interactable component
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Set up audio source if not assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        // Subscribe to grab events
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
    }

    void Update()
    {
        // Check if object has moved far enough from original position
        if (isGrabbed && spawnOnDistance && !hasSpawned)
        {
            float distance = Vector3.Distance(transform.position, originalPosition);
            if (distance > spawnDistanceThreshold)
            {
                SpawnNewObject();
            }
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;

        // Spawn immediately on grab if enabled
        if (spawnOnGrab && !hasSpawned)
        {
            SpawnNewObject();
        }
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
    }

    void SpawnNewObject()
    {
        if (hasSpawned || objectPrefab == null) return;

        // Create new object at original position
        GameObject newObject = Instantiate(objectPrefab, originalPosition, originalRotation);

        // Play spawn sound
        if (audioSource != null && spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }

        hasSpawned = true;

        // Optional: Destroy this object after a delay
        // Destroy(gameObject, 5f);
    }

    void OnDestroy()
    {
        // Unsubscribe from events
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    // Public method to reset the spawn state (useful for testing)
    public void ResetSpawnState()
    {
        hasSpawned = false;
    }

    // Public method to manually spawn (useful for testing)
    public void ManualSpawn()
    {
        if (!hasSpawned)
        {
            SpawnNewObject();
        }
    }
}