using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallLauncher : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;

    public XRMenuController xrmenucontroller;

    public InputActionReference spawnBallAction;

    public Transform battingArea; //  center of the grey area
    public Vector2 areaSize = new Vector2(2f, 2f); //  dimensions of the grey area

    public TextMeshProUGUI ballCountText;

    public delegate void BallCountChanged(int ballCount, int maxballs);
    public static event BallCountChanged OnBallCountChanged;
    private int ballCount = 0;
    public int maxBalls = 30;

    public float launchForce = 10f;
    public float upwardAngle = 0f;
    public float launchInterval = 10f;
    public float ballLifetime = 10f;

    public bool RandomLaunchAngle = false;

    /*
        void Start()
        {
            InvokeRepeating(nameof(LaunchBall), 2f, launchInterval);
        } */

    void OnEnable()
    {
        spawnBallAction.action.performed += LaunchBall;
        spawnBallAction.action.Enable();
        ScoreManager.OnReplayRequested += ResetBalls;
    }

    void OnDisable()
    {
        spawnBallAction.action.performed -= LaunchBall;
        spawnBallAction.action.Disable();
        ScoreManager.OnReplayRequested -= ResetBalls;
    }

    void LaunchBall(InputAction.CallbackContext context)
    {
        if (ballCount >= maxBalls)
        {
            return;
        }
        if (
            xrmenucontroller.isMenuOpen
            || ZoneManager.Instance.activeZone != "Cricket"
            || StumpManager.Instance.fallenStumps > 0
        )
        {
            return;
        }
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);

        Vector3 randomOffset = new Vector3(
            Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
            0f,
            Random.Range(-areaSize.y / 2f, areaSize.y / 2f)
        );
        Vector3 target = battingArea.position + randomOffset;

        Vector3 toTarget = target - spawnPoint.position;
        Vector3 toTargetXZ = new Vector3(toTarget.x, 0, toTarget.z);
        float distanceXZ = toTargetXZ.magnitude;
        float heightDifference = toTarget.y;

        if (RandomLaunchAngle)
        {
            upwardAngle = Random.Range(5f, 30);
        }
        float angle = upwardAngle * Mathf.Deg2Rad;
        float gravity = Mathf.Abs(Physics.gravity.y);

        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);

        float underSqrt = distanceXZ * Mathf.Tan(angle) - heightDifference;
        if (underSqrt <= 0)
        {
            Debug.LogWarning("Launch not possible due to distance angle velocity mismatch");
            Destroy(ball);
            return;
        }

        float velocity = distanceXZ / (cos) * Mathf.Sqrt(0.5f * gravity / underSqrt);

        Vector3 velocityVec = toTargetXZ.normalized * velocity * cos + Vector3.up * velocity * sin;

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.linearVelocity = velocityVec;

        ballCount++;
        OnBallCountChanged?.Invoke(ballCount, maxBalls);
        Debug.Log("BallLauncher event fired with count: " + ballCount);
        UpdateBallUI();

        Destroy(ball, ballLifetime);
    }

    public void ResetBalls()
    {
        ballCount = 0;
        UpdateBallUI();
    }

    void UpdateBallUI()
    {
        if (ballCountText != null)
        {
            ballCountText.text = "Balls: " + ballCount;
        }
    }
}
