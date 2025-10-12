using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class StumpManager : MonoBehaviour
{
    public static StumpManager Instance;
    public ScoreManager scoremanager;

    [HideInInspector]
    public int fallenStumps = 0;
    private bool alreadyReported = false;

    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;

    public UnityEvent OnAnyStumpFallen;

    public InputActionReference resetAction;

    private void Awake()
    {
        Instance = this;
        int count = transform.childCount;
        initialPositions = new Vector3[count];
        initialRotations = new Quaternion[count];

        for (int i = 0; i < count; i++)
        {
            Transform stump = transform.GetChild(i);
            initialPositions[i] = stump.position;
            initialRotations[i] = stump.rotation;
        }
    }

    private void OnEnable()
    {
        resetAction.action.performed += OnReset;
        resetAction.action.Enable();
    }

    private void OnDisable()
    {
        resetAction.action.performed -= OnReset;
        resetAction.action.Disable();
    }

    private void OnReset(InputAction.CallbackContext context)
    {
        if (StumpManager.Instance != null)
        {
            Debug.Log("Reset stumps input triggered!");
            ResetStumps();
        }
    }

    public void OnStumpFallen(StumpFall stump)
    {
        fallenStumps++;

        if (!alreadyReported)
        {
            alreadyReported = true;
            Debug.Log("one stump fallen");
            OnAnyStumpFallen?.Invoke();
            Invoke(nameof(HandleWicketFall), 9f);
        }
    }

    private void HandleWicketFall()
    {
        scoremanager.ShowBanner("Reset Stumps by pressing 'X'");
        Debug.Log("pausing game due to wicket");
    }

    public void ResetStumps()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform pin = transform.GetChild(i);

            pin.position = initialPositions[i];
            pin.rotation = initialRotations[i];

            Rigidbody rb = pin.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.Sleep();
            }
        }

        fallenStumps = 0;
        alreadyReported = false;
    }
}
