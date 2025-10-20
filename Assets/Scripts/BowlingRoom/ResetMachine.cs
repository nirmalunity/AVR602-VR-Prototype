using System.Collections;
using UnityEngine;

public class BowlingPinResetMachine : MonoBehaviour
{
    [Header("Top-Bottom-Position")]
    public Transform topPosition;
    public Transform bottomPosition;
    public float moveDuration = 2f;
    public float pauseTime = 1f;

    [Header("Pipes")]
    public Transform[] pipes; // Assign both pipes
    public float pipeStartScale = 1f;
    public float pipeEndScale = 3f;

    private Vector3[] pipeInitialPositions;
    private bool isMoving = false;

    // For testing
    [Header("Testing")]
    public bool autoTest = false;
    public float testInterval = 5f;
    private float timer = 0f;

    void Start()
    {
        if (pipes != null && pipes.Length > 0)
        {
            pipeInitialPositions = new Vector3[pipes.Length];
            for (int i = 0; i < pipes.Length; i++)
                pipeInitialPositions[i] = pipes[i].localPosition;
        }
    }

    /*     void Update()
        {
            if (!autoTest)
                return;
    
            timer += Time.deltaTime;
            if (timer >= testInterval)
            {
                timer = 0f;
                ResetPins();
            }
        }
     */
    public void ResetPinMachine()
    {
        if (!isMoving)
            StartCoroutine(MoveSequence());
    }

    private IEnumerator MoveSequence()
    {
        isMoving = true;

        // Move down
        yield return StartCoroutine(
            MoveWithPipes(
                topPosition.position,
                bottomPosition.position,
                pipeStartScale,
                pipeEndScale
            )
        );

        yield return new WaitForSeconds(pauseTime);

        // Move up
        yield return StartCoroutine(
            MoveWithPipes(
                bottomPosition.position,
                topPosition.position,
                pipeEndScale,
                pipeStartScale
            )
        );

        isMoving = false;
    }

    private IEnumerator MoveWithPipes(
        Vector3 startPos,
        Vector3 endPos,
        float startScale,
        float endScale
    )
    {
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            // Move machine smoothly
            transform.position = Vector3.Lerp(startPos, endPos, smoothT);

            // move  pipes slowly from top
            if (pipes != null && pipes.Length > 0)
            {
                for (int i = 0; i < pipes.Length; i++)
                {
                    Transform pipe = pipes[i];
                    if (pipe == null)
                        continue;

                    float currentScale = Mathf.Lerp(startScale, endScale, smoothT);

                    pipe.localScale = new Vector3(
                        pipe.localScale.x,
                        currentScale,
                        pipe.localScale.z
                    );

                    float offsetY = (currentScale - 1f) * 0.5f * pipe.localScale.y;
                    pipe.localPosition = pipeInitialPositions[i] - new Vector3(0f, offsetY, 0f);
                }
            }

            yield return null;
        }

        // fix final position
        transform.position = endPos;

        if (pipes != null && pipes.Length > 0)
        {
            for (int i = 0; i < pipes.Length; i++)
            {
                Transform pipe = pipes[i];
                if (pipe == null)
                    continue;

                pipe.localScale = new Vector3(pipe.localScale.x, endScale, pipe.localScale.z);
                float offsetY = (endScale - 1f) * 0.5f * pipe.localScale.y;
                pipe.localPosition = pipeInitialPositions[i] - new Vector3(0f, offsetY, 0f);
            }
        }
    }
}
