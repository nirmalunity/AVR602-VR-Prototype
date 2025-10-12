using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    public int currentWickets = 0;
    public TextMeshProUGUI scoreBanner;
    public TextMeshProUGUI scoreText;

    public float displayDuration = 2f;

    public AudioSource audioSource;
    public AudioClip sixRunClip;
    public AudioClip fourRunClip;
    public AudioClip bowledClip;

    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    public static event Action OnReplayRequested;

    public VideoPlayer videoPlayer;
    public VideoPlayer FourvideoPlayer;
    public GameObject videoCanvas;
    public GameObject FourvideoCanvas;

    public VideoPlayer bowledplayer;
    public GameObject BowledCanvas;

    private void Start()
    {
        if (scoreBanner != null)
            scoreBanner.gameObject.SetActive(false);

        UpdateUI();
    }

    /*     private void Awake()
        {
            BallLauncher.OnBallCountChanged += CheckGameOver;
        } */

    public void AddScore(int value)
    {
        if (value == 0)
        {
            currentWickets++;
            ShowBanner("Out !");
        }
        else if (value == -1)
        {
            currentWickets++;
            // GetComponent<AudioFader>().PlayAndFadeOut(bowledClip);
            GetComponent<AudioSource>().PlayOneShot(bowledClip);
            Invoke(nameof(PlayBowledEffects), 8f);
            /*    ShowBanner("Bowled!");
               UpdateUI(); */
        }
        else
        {
            if (value == 6 && audioSource != null && sixRunClip != null)
            {
                //ShowCelebration();
                StartCoroutine(PlayCelebrationThenAudio(sixRunClip, value));

                //  GetComponent<AudioFader>().PlayAndFadeOut(sixRunClip);
            }
            else if (value == 4 && audioSource != null && fourRunClip != null)
            {
                // ShowCelebration();
                StartCoroutine(PlayCelebrationThenAudio(fourRunClip, value));
                // GetComponent<AudioFader>().PlayAndFadeOut(fourRunClip);
            }
            else
            {
                currentScore += value;
                ShowBanner(" " + value + " !");
                UpdateUI();
            }
        }

        if (currentWickets >= 10)
        {
            EndGame();
        }
    }

    private IEnumerator PlayCelebrationThenAudio(AudioClip clip, int value)
    {
        ShowCelebration(value);
        yield return new WaitForSeconds(8f);
        GetComponent<AudioFader>().PlayAndFadeOut(clip);
        currentScore += value;
        ShowBanner(" " + value + " !");
        UpdateUI();
    }

    private void ShowCelebration(int value)
    {
        if (value == 6)
        {
            if (videoCanvas != null)
                videoCanvas.SetActive(true);

            if (videoPlayer != null)
            {
                videoPlayer.Stop();
                videoPlayer.Play();
            }
        }
        else if (value == 4)
        {
            if (FourvideoCanvas != null)
                FourvideoCanvas.SetActive(true);

            if (FourvideoPlayer != null)
            {
                FourvideoPlayer.Stop();
                FourvideoPlayer.Play();
            }
        }
        Invoke(nameof(HideVideo), 8f);
    }

    public void ShowBowled()
    {
        if (BowledCanvas != null)
        {
            BowledCanvas.SetActive(true);

            if (bowledplayer != null)
            {
                bowledplayer.Stop();
                bowledplayer.Play();
            }

            Invoke(nameof(HideBowled), 7f);
        }
    }

    private void HideBowled()
    {
        if (BowledCanvas != null)
            BowledCanvas.SetActive(false);
    }

    private void HideVideo()
    {
        if (videoCanvas != null)
            videoCanvas.SetActive(false);

        if (FourvideoCanvas != null)
            FourvideoCanvas.SetActive(false);
    }

    private void PlayBowledEffects()
    {
        GetComponent<AudioSource>().PlayOneShot(bowledClip);
        ShowBanner("Bowled!");
        UpdateUI();
    }

    private void EndGame()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            finalScoreText.text = "Final Score: " + currentScore + "/" + currentWickets;
        }
    }

    public void ReplayGame()
    {
        currentScore = 0;
        currentWickets = 0;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        OnReplayRequested?.Invoke();

        UpdateUI();
    }

    public void ShowBanner(string message)
    {
        if (scoreBanner != null)
        {
            scoreBanner.fontSize = message.Length < 5 ? 36 : 20;
            scoreBanner.text = message;
            scoreBanner.gameObject.SetActive(true);
            CancelInvoke(nameof(HideBanner));
            Invoke(nameof(HideBanner), displayDuration);
        }
    }

    private void HideBanner()
    {
        if (scoreBanner != null)
            scoreBanner.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = currentScore + "/" + currentWickets;
    }

    private void OnEnable()
    {
        BallLauncher.OnBallCountChanged += CheckGameOver;
    }

    private void OnDisable()
    {
        BallLauncher.OnBallCountChanged -= CheckGameOver;
    }

    private void CheckGameOver(int currentBalls, int maxBalls)
    {
        Debug.Log("ScoreManager received ball count: " + currentBalls);
        if (currentBalls >= maxBalls)
        {
            EndGame();
        }
    }
}
