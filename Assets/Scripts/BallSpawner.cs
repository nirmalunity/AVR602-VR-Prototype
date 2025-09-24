using UnityEngine;
using UnityEngine.InputSystem;

public class BallSpawnerXR : MonoBehaviour
{
    public InputActionReference spawnBallAction;
    public GameObject ballPrefab;
    public Transform playerTransform;

    public AudioClip balldropsound;
    public AudioSource audioSource;

    void OnEnable()
    {
        spawnBallAction.action.performed += SpawnBall;
        spawnBallAction.action.Enable();
    }

    void OnDisable()
    {
        spawnBallAction.action.performed -= SpawnBall;
        spawnBallAction.action.Disable();
    }

    void SpawnBall(InputAction.CallbackContext context)
    {
        audioSource.PlayOneShot(balldropsound);
        Instantiate(ballPrefab, playerTransform.position + playerTransform.forward * 2f, Quaternion.identity);

    }
}
