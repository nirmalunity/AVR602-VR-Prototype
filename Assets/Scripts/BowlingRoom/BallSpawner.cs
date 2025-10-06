using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public int maxBalls = 5;

    public float spawnInterval = 2f;
    private List<GameObject> activeBalls = new List<GameObject>();

    void Start()
    {
        InvokeRepeating(nameof(SpawnBall), 1f, spawnInterval);
    }

    void SpawnBall()
    {
        activeBalls.RemoveAll(ball => ball == null);

        if (activeBalls.Count < maxBalls)
        {
            GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
            activeBalls.Add(ball);
        }
    }
}
