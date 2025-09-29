using UnityEngine;
using UnityEngine.InputSystem;

public class BallLauncher : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;

    public InputActionReference spawnBallAction;

    public Transform battingArea; //  center of the grey area
    public Vector2 areaSize = new Vector2(2f, 2f); //  dimensions of the grey area

    public float launchForce = 10f;
    public float upwardAngle = 0f;
    public float launchInterval = 10f;
    public float ballLifetime = 10f;
/* 
    void Start()
    {
        InvokeRepeating(nameof(LaunchBall), 2f, launchInterval);
    } */


  void OnEnable()
    {
        spawnBallAction.action.performed += LaunchBall;
        spawnBallAction.action.Enable();
    }

    void OnDisable()
    {
        spawnBallAction.action.performed -= LaunchBall;
        spawnBallAction.action.Disable();
    }


void LaunchBall(InputAction.CallbackContext context)
{
   
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

    float angle = upwardAngle * Mathf.Deg2Rad;
    float gravity = Mathf.Abs(Physics.gravity.y);

  
    float cos = Mathf.Cos(angle);
    float sin = Mathf.Sin(angle);

   
    float underSqrt = distanceXZ * Mathf.Tan(angle) - heightDifference;
    if (underSqrt <= 0) {
        Debug.LogWarning("Launch not possible due to distance angle velocity mismatch");
        Destroy(ball);
        return;
    }

    float velocity = distanceXZ / (cos) * Mathf.Sqrt(0.5f * gravity / underSqrt);


    Vector3 velocityVec = toTargetXZ.normalized * velocity * cos + Vector3.up * velocity * sin;

 
    Rigidbody rb = ball.GetComponent<Rigidbody>();
    rb.linearVelocity = velocityVec;


    Destroy(ball, ballLifetime);
}



}
