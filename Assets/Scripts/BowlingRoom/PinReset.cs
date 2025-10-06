using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    private Vector3[] initialPositions;
    private Quaternion[] initialRotations;

    public GameObject gameOverPanel;

    private List<int> allThrows = new List<int>();
    private int throwCount = 0;
    private int pinsKnockedDown = 0;
    private int currentFrame = 0;
    int totalScore = 0;

    public TextMeshPro[] scoreTexts;
    public TMP_Text FinalScore;
    public float fallenThreshold = 60f;

    private void Start()
    {
        int childCount = transform.childCount;
        initialPositions = new Vector3[childCount];
        initialRotations = new Quaternion[childCount];

        for (int i = 0; i < childCount; i++)
        {
            Transform pin = transform.GetChild(i);
            initialPositions[i] = pin.position;
            initialRotations[i] = pin.rotation;
        }
    }

    private void ResetScoreBoard()
    {
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            scoreTexts[i].text = "F" + (i + 1);
        }
        if (FinalScore != null)
        {
            FinalScore.text = "Total: " + totalScore.ToString();
        }
    }

    public void OnThrow()
    {
        throwCount++;

        int fallenPins = CountFallenPins();
        int pinsHit = fallenPins - pinsKnockedDown;
        pinsKnockedDown = fallenPins;

        allThrows.Add(pinsHit);
        // STRIKE
        if (throwCount == 1 && fallenPins == transform.childCount)
        {
            scoreTexts[currentFrame].text = "X";
            NextFrame();
            ResetPins();
            return;
        }

        // SPARE
        if (throwCount == 2 && fallenPins == transform.childCount)
        {
            scoreTexts[currentFrame].text = "/";
            NextFrame();
            ResetPins();
            return;
        }

        // OPEN FRAME
        if (throwCount == 2)
        {
            scoreTexts[currentFrame].text = fallenPins.ToString();
            NextFrame();
            ResetPins();
        }
    }

    private int CountFallenPins()
    {
        int fallen = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform pin = transform.GetChild(i);
            float tilt = Vector3.Angle(Vector3.up, pin.forward);

            if (tilt > fallenThreshold)
            {
                fallen++;
            }
        }

        return fallen;
    }

    private void CalculateScore()
    {
        int throwIndex = 0;
        totalScore = 0;

        for (int frame = 0; frame < 10; frame++)
        {
            if (throwIndex >= allThrows.Count)
                break;

            if (allThrows[throwIndex] == 10) // strike
            {
                int strikeBonus = 0;
                if (throwIndex + 1 < allThrows.Count)
                {
                    strikeBonus += allThrows[throwIndex + 1];
                }
                if (throwIndex + 2 < allThrows.Count)
                {
                    strikeBonus += allThrows[throwIndex + 2];
                }

                totalScore += allThrows[throwIndex] + strikeBonus;
                throwIndex += 1;
            }
            else if (
                throwIndex + 1 < allThrows.Count
                && allThrows[throwIndex] + allThrows[throwIndex + 1] == 10
            )
            {
                int spareBonus = 0;

                if (throwIndex + 2 < allThrows.Count)
                {
                    spareBonus += allThrows[throwIndex + 2];
                }
                totalScore += spareBonus + 10;
                throwIndex += 2;
            }
            else
            {
                totalScore += allThrows[throwIndex] + allThrows[throwIndex + 1];
                throwIndex += 2;
            }
        }

        if (FinalScore != null)
        {
            FinalScore.text = "Total: " + totalScore.ToString();
        }
    }

    private void ResetPins()
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

        pinsKnockedDown = 0; // reset fallen count
        throwCount = 0;
    }

    public void Replay()
    {
        throwCount = 0;
        currentFrame = 0;
        totalScore = 0;
        allThrows.Clear();
        pinsKnockedDown = 0;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        ResetScoreBoard();
        ResetPins();
    }

    private void NextFrame()
    {
        throwCount = 0;
        currentFrame++;

        CalculateScore();

        if (currentFrame >= scoreTexts.Length)
        {
            Debug.Log("Game Over!");
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }
    }
}
